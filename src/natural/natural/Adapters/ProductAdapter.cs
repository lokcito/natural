using System;
using System.Collections.Generic;

using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RestSharp;
using RestSharp.Deserializers;
using SimpleJson;
using natural.Models;
using System.Net;

namespace natural.Adapters
{
    class ProductAdapter : BaseAdapter<Product>
    {

        Context context;
        List<Product> list;

        public ProductAdapter(Context context, List<Product> _list)
            : base()
        {
            this.context = context;
            this.list = _list;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            ProductAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as ProductAdapterViewHolder;

            if (holder == null)
            {
                holder = new ProductAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.adapter_product_item, parent, false);
                holder.Name = view.FindViewById<TextView>(Resource.Id.textName);
                holder.Image = view.FindViewById<ImageView>(Resource.Id.imageProduct);
                view.Tag = holder;
            }


            //fill in your items
            holder.Name.Text = list[position].last_name;
            Bitmap imageBitmap = GetImageBitmapFromUrl(list[position].avatar);
            holder.Image.SetImageBitmap(imageBitmap);
            //holder.Image.SetColorFilter(Color.ParseColor(list[position].color + "00"));// Background.set = list[position].color;

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return list.Count;
            }
        }

        public override Product this[int position]
        {
            get
            {
                return list[position];
            }
        }
        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }

    class ProductAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        public TextView Name { get; set; }
        public ImageView Image { get; set; }
    }
}