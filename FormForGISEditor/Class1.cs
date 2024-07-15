using SGP.Gis;
using SGP.Gis.Attributes;
using SGP.Gis.Carto.Layers;
using SGP.Gis.Commands;
using SGP.Gis.Controls;
using SGP.Gis.Features;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;

namespace FormForGISEditor
{
    [GisApplication(GisApplicationType.Map)]
    [Guid("AEA4F60B-1B23-4E06-8784-C739C51E0829")]
    [BarItem(true, 5, CommandIconSize.Large, true)]

    public class SampleSelectLayerButton : ButtonCommandInfo
    {
        public SampleSelectLayerButton() : base("��������", "�����", "���������� ��������", SGP.Gis.Resources.Icons.IconType.Run)
        {
            ToolTip = "����� ��� ����������� ���������� �������� ��������";
            IsEnabled = true;
            IsVisible = true;
        }

        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            //��������� �����
            var mapControl = Application.Context.Resolve<IMapControl>();

            var layersModel = new SampleLayersWindowModel
            {
                Map = mapControl
            };

            var result = Application.WindowManager.ShowWindow(new WindowParameters
            {
                //��������� ����������� ���� �� ������
                Content = new ObjectControlGenerated
                {
                    //�������
                    Margin = new Thickness(30),
                    //������ ����������� ����
                    SelectedObject = layersModel
                },
                Width = 700,
                Height = 600,
                Title = "���������� ��������"
            });
        }
    }

    //����������� ������ ����
    public class SampleLayersWindowModel
    {
        public IField FieldList { get; set; }
        public IMapControl Map { get; set; }
        public ILayer[]? LayerList
        {
            get
            {
                return Map?.Document.GetAllLayers()
                                  .OfType<ILayer>()
                                  .ToArray();
            }
        }

        [Browsable(true)]
        [DisplayName("�������� ���� �� ������:")]
        [ItemsSourceBinding(nameof(LayerList), DisplayMember = "Name")]
        public ILayer SelectedLayer { get; set; }

        [Browsable(true)]
        [DisplayName("")]
        [ReadOnly(true)]
        public string TextReadOnly { get; } = "������� ���� ��� ����������� ��������";

        [Browsable(true)]
        [DisplayName("�������� ���� 1:")]
        [ItemsSourceBinding(nameof(FieldList), DisplayMember = "Name")]
        public IField SelectedField1 { get; set; }

        [Browsable(true)]
        [DisplayName("�������� ���� 2:")]
        [ItemsSourceBinding(nameof(FieldList), DisplayMember = "Name")]
        public IField SelectedField2 { get; set; }

        [Browsable(true)]
        [DisplayName("���������")]
        public IButton ActionButton { get; set; }



        /*[Browsable(true)]
        [DisplayName("�������")]
        [Field(nameof(StatisticsTable), Name = "����")]
        public DataTable StatisticsTable
            {
                get
                {
                    try
                    {                        
                        Type myType = typeof(ViewTechnology);
                        
                        FieldInfo[] myField = myType.GetFields();
                       
                        for (int i = 0; i < myField.Length; i++)
                        {                            
                            if (myField[i].IsSpecialName)
                            {
                                
                                    myField[i].Name);
                            }
                        }
                    }
                    catch (Exception e)
                {

                }
                }
            }*/
       /* public void CalculateStatistics()
        {
            if (SelectedLayer == null || SelectedField1 == null) return;

            var featureCursor = SelectedLayer();

            var statistics = new Dictionary<string, List<double>>();

            while (featureCursor.MoveNext())
            {
                var feature = featureCursor.Current;
                var fieldValue1 = feature.GetFieldValue(SelectedField1.Name).ToString();
                var fieldValue2 = SelectedField2 != null ? feature.GetFieldValue(SelectedField2.Name).ToString() : string.Empty;

                var key = fieldValue1 + (fieldValue2 != string.Empty ? "_" + fieldValue2 : string.Empty);

                if (!statistics.ContainsKey(key))
                {
                    statistics.Add(key, new List<double>());
                }

                var area = feature.Shape.Area;
                statistics[key].Add(area);
            }

            featureCursor.Close();

            var dataTable = new DataTable();
            dataTable.Columns.Add("���� 1", typeof(string));
            dataTable.Columns.Add("���� 2", typeof(string));
            dataTable.Columns.Add("����������� �������", typeof(double));
            dataTable.Columns.Add("������������ �������", typeof(double));
            dataTable.Columns.Add("��������� �������", typeof(double));

            foreach (var group in statistics)
            {
                var row = dataTable.NewRow();
                row["���� 1"] = group.Key;
                row["���� 2"] = group.Key;
                row["����������� �������"] = group.Value.Min();
                row["������������ �������"] = group.Value.Max();
                row["��������� �������"] = group.Value.Sum();
                dataTable.Rows.Add(row);
            }

            StatisticsTable = dataTable;
        }*/
    }
}