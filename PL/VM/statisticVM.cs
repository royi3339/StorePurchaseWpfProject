using BE;
using BL;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using PL.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static BE.myEnums;

namespace PL.VM
{
    public class statisticVM : viewModel
    {
        public BLimp myBL = new BLimp();
        public SeriesCollection seriesCollection { get; set; }

        Util util = new Util();

        public ObservableCollection<string> AxisXLabels { get; set; }

        public ChartValues<float> setOfCost { get; set; }

        int aggreggtion;

        Func<DateTime, int> foo;

        private bool _StatisticVisible;
        public bool statisticVisible
        {
            get { return _StatisticVisible; }
            set
            {
                _StatisticVisible = value;
                update("statisticVisible");
            }
        }

        private Func<double, string> _YFormatter;
        public Func<double, string> YFormatter
        {
            get { return _YFormatter; }
            set
            {
                _YFormatter = value;
                update("YFormatter");
            }
        }

        private string _AxisYTitle;
        public string AxisYTitle
        {
            get { return _AxisYTitle; }
            set
            {
                _AxisYTitle = value;
                update("AxisYTitle");
            }
        }

        // 0 - reccomendetionTable.
        // 1 - lineChart. 
        // 2 - barChartCost.
        private bool[] _ChartStatus;
        public bool[] chartStatus
        {
            get { return _ChartStatus; }
            set
            {
                _ChartStatus = value;
                update("chartStatus");
            }
        }

        private int _TypeComboIndex;
        public int typeComboIndex
        {
            get { return _TypeComboIndex; }
            set
            {
                _TypeComboIndex = value;
                update("typeComboIndex");                
                updateTypeIndex();
            }
        }

        private void updateTypeIndex()
        {
            switch (_TypeComboIndex)
            {
                // Products Amount.
                case (0):
                // Categorie Amount.
                case (1):
                    changeBoolVisibility(1);
                    YFormatter = value => value + "Pcs";
                    AxisYTitle = "amount";
                    break;
                // Cost.
                case (2):
                    changeBoolVisibility(2);
                    YFormatter = value => value + "₪";
                    AxisYTitle = "cost";
                    break;
            }
            if (foo != null)
            {
                updateChart();
            }
        }

        private int _AggreggationComboIndex;
        public int aggreggationComboIndex
        {
            get { return _AggreggationComboIndex; }
            set
            {
                _AggreggationComboIndex = value;
                update("aggreggationComboIndex");

                switch (_AggreggationComboIndex)
                {
                    // Days Of Week.
                    case (0): aggreggtion = 7; foo = getDayWeek;
                        AxisXLabels.Clear(); 
                        foreach (var day in Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>())
                        {
                            AxisXLabels.Add(day.ToString());
                        }
                        break;
                    // Days Of Month.
                    case (1): aggreggtion = 31; foo = getDayMonth;
                        AxisXLabels.Clear();
                        AxisXLabels.Add("1st");
                        AxisXLabels.Add("2nd");
                        AxisXLabels.Add("3rd");
                        for (int i = 3; i < aggreggtion; i++)
                        {
                            AxisXLabels.Add(i + 1 + "th");
                        }
                        break;
                    // Hours Of Day.
                    case (2): aggreggtion = 24; foo = getHours;
                        AxisXLabels.Clear();
                        for (int i = 0; i < aggreggtion; i++)
                        {
                            AxisXLabels.Add(i + ":00");
                        }
                        break;
                }
                if (chartStatus[0] || foo == null)
                {
                    updateTypeIndex();
                }
                updateChart();
            }
        }

        public List<string> typeComboOption { get; set; }
        public List<string> aggreggationComboOption { get; set; }
        public genericCommand loadRulesB { get; set; }
        public ObservableCollection<string> rulesList { get; set; }
              
        public statisticVM()
        {
            setOfCost = new ChartValues<float>();
            seriesCollection = new SeriesCollection();

            chartStatus = new bool[3];

            AxisXLabels = new ObservableCollection<string>();

            aggreggationComboOption = new List<string>() { "Days Of Week", "Days Of Month", "Hours Of Day" };
            typeComboOption = new List<string>() { "Products Amount", "Categorie Amount", "Cost" };
            typeComboIndex = 0;
            aggreggationComboIndex = 0;

            rulesList = new ObservableCollection<string>(myBL.getRulesAsString());

            loadRulesB = new genericCommand();
            loadRulesB.genericClickEvent += loadRulesFoo;
        }

        public void updateChart()
        {
            Action<int, Func<DateTime, int>>[] funcs = new Action<int, Func<DateTime, int>>[] { lineChartProducts, lineChartCategorie, barChartCost };       
            funcs[typeComboIndex](aggreggtion, foo);
        }

        public void lineChartCategorie(int aggreggation, Func<DateTime, int> foo)
        {
            seriesCollection.Clear();

            List<categorieType> productsCategories = myBL.GetProducts().Select(y => y.categorie).Distinct().ToList();

            foreach (categorieType categorie in productsCategories)
            {
                List<OrderRow> allOrderRows = myBL.getAllOrderRows().Where(y => myBL.convertIdToProduct(y.productId).categorie == categorie).ToList();
                ChartValues<ObservablePoint> v = new ChartValues<ObservablePoint>();
                int[] daysArr = new int[aggreggation];

                foreach (OrderRow row in allOrderRows)
                {
                    daysArr[foo(getTime(row.orderId))] += row.amount;
                }

                for (int i = 0; i < aggreggation; i++)
                {
                    v.Add(new ObservablePoint(i, daysArr[i]));
                }
                seriesCollection.Add(new LineSeries { Values = v, Title = categorie.ToString() });
            }
        }

        public void barChartCost(int aggreggation, Func<DateTime, int> foo)
        {
            setOfCost.Clear();
            List<Order> orders = myBL.getAllOrders();
            float[] f = new float[aggreggation];
            foreach (Order order in orders)
            {
                f[foo(order.orderDate)] += order.orderPrice;
            }
            for (int i = 0 ; i < aggreggation ; i++)
            {
                setOfCost.Add(f[i]);
            }
        }


        public void lineChartProducts(int aggreggation, Func<DateTime, int> foo)
        {
            seriesCollection.Clear();

            List<int> productsId = myBL.GetProducts().Select(y => y.id).ToList();

            // First foreach over on all of the Products.
            foreach (int id in productsId)
            {
                List<OrderRow> allOrderRows = myBL.getAllOrderRows().Where(y => y.productId == id).ToList();
                ChartValues<ObservablePoint> v = new ChartValues<ObservablePoint>();
                int[] daysArr = new int[aggreggation];

                // Second foreach over on all of time that bought a specific Product.
                foreach (OrderRow row in allOrderRows)
                {
                    daysArr[foo(getTime(row.orderId))] += row.amount;
                }

                // Third for adding this point which contain the Ptoduct and its amount to the array.
                for (int i = 0; i < aggreggation; i++)
                {
                    v.Add(new ObservablePoint(i, daysArr[i]));
                }
                seriesCollection.Add(new LineSeries { Values = v, Title = myBL.convertIdToProduct(id).name });
            }
        }

        private DateTime getTime(int orderId)
        {
            return myBL.getAllOrders().Where(y => y.id == orderId).Select(x => x.orderDate).FirstOrDefault();
        }

        private int getDayWeek(DateTime time) { return util.convertDay(time) * (-1) - 1; }

        private int getDayMonth(DateTime time) { return time.Day - 1; }

        private int getHours(DateTime time) { return time.Hour; }

        public void changeBoolVisibility(int num)
        {
            bool[] temp = new bool[3];
            for (int i = 0; i < 3; i++)
            {
                temp[i] = i == num ? true : false;
            }
            chartStatus = temp;
        }

        private void loadRulesFoo()
        {
            rulesList.Clear();
            foreach (string rule in myBL.getRulesAsString())
            {
                rulesList.Add(rule);
            }              
            changeBoolVisibility(0);           
        }
    }
}
