using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;
using GMap.NET;

namespace ControlDemoApp
{
    public partial class Map : Form
    {
        public Map()
        {
            InitializeComponent();
        }

        private void Map_Load(object sender, EventArgs e)
        {
            GMapProviders.GoogleMap.ApiKey = @"AIzaSyAGadHkPEC69Ye2UPUiitdOyTtgx813JIc";
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            double lat = MainForm.passing_text1;
            double lng = MainForm.passing_text2;
            gMapControl1.Position = new GMap.NET.PointLatLng(lat,lng);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 15;
            gMapControl1.Zoom = 15;
            
            PointLatLng point = new PointLatLng(lat, lng);
            GMapMarker marker = new GMarkerGoogle(point,GMarkerGoogleType.red_dot);
            GMapOverlay markers = new GMapOverlay("markers");
            gMapControl1.Overlays.Add(markers);
            markers.Markers.Add(marker);            
        }
    }
}
