using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using natural.Adapters;
using natural.Models;

namespace natural.Fragments
{
    public class FragmentProducts : Fragment
    {
        ListView listViewUsers;
        List<Product> productList = new List<Product>();
        MainActivity parent = null;
        View v;
        public FragmentProducts(MainActivity _parent) : base()
        {
            parent = _parent;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            v = inflater.Inflate(Resource.Layout.fragment_products, container, false);

            listViewUsers = v.FindViewById<ListView>(Resource.Id.listView);
            listViewUsers.ItemClick += onSelectItem;

            load();




            return v;

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
        public async void load ()
        {
            Task.Factory.StartNew(() => {
                // Do some work on a background thread, allowing the UI to remain responsive
                productList = Product.sync();
                // When the background work is done, continue with this code block
            }).ContinueWith(task => {
                render(productList);
                //DoSomethingOnTheUIThread();
                // the following forces the code in the ContinueWith block to be run on the
                // calling thread, often the Main/UI thread.
            }, TaskScheduler.FromCurrentSynchronizationContext());

  
        }
        private void onSelectItem(object sender, AdapterView.ItemClickEventArgs e)
        {
            Product obj = productList[e.Position];
            Dictionary<string, string> o = new Dictionary<string, string>()
            {
                {"id", String.Format("{0}", obj.id)}
            };
            parent.changeFrame("detail_products", o);
        }
        public int render(List<Product> list)
        {
            //productList = list;

            ProductAdapter userAdapter = new ProductAdapter(v.Context, list);
            listViewUsers.Adapter = userAdapter;

            return 0;
        }
    }
}