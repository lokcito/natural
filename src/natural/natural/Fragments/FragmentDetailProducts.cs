using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    public class FragmentDetailProducts : Fragment
    {
        Product productObj = new Product();
        MainActivity parent = null;
        Button btnText = null;
        public FragmentDetailProducts(MainActivity _parent) : base()
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
            View v = inflater.Inflate(Resource.Layout.fragment_detail_products, container, false);
            btnText = v.FindViewById<Button>(Resource.Id.button1);
            String _id = Arguments.GetString("id");
            int id = 0;
            if (_id == null)
            {
                id = 1;
            }
            else {
                int.TryParse(_id, out id);
            }


            //client.ExecuteAsync(
            //    request,
            //    response =>
            //    {
            //        responseResult = response.Content;

            //        var newrequest = new RestRequest("api/Subscription", Method.POST);
            //        newrequest.AddParameter("phoneId", id);
            //        newrequest.AddParameter("channelUri", uri);
            //        client.ExecuteAsync(
            //            newrequest,
            //            newresponse =>
            //            {
            //                responseResult = newresponse.Content;
            //            });
            //    });

            Product.get(id, render);
            
            //System.Diagnostics.Debug.Print(">>>" + stringData);

            return v;

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public int render(Product o) {
            btnText.Text = o.first_name;
            return 0;
        }
    }
}