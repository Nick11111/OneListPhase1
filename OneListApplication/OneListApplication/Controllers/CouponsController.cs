using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OneListApplication;
using System.Web.Http.Cors;
using OneListApplication.ViewModels;

namespace OneListApplication.Controllers
{
    public class CouponsController : ApiController
    {
       
        private OneListEntitiesCore db = new OneListEntitiesCore();

        // GET: api/Coupons
        
        public IQueryable<CouponsVM> GetCoupons()
        {
            db.Configuration.ProxyCreationEnabled = false;
            IEnumerable<Coupon> coupons =  db.Coupons.Where(p => p.EndingDate>= DateTime.Today).Select(p=>p);
            List<CouponsVM> couponsFinal = new List<CouponsVM>();
            foreach (Coupon coupon in coupons)
            {
                CouponsVM singleCoupon = new CouponsVM();
                singleCoupon.CouponID = coupon.CouponID;
                singleCoupon.Description = coupon.Description;
                singleCoupon.DiscountPercentage = coupon.DiscountPercentage;
                singleCoupon.EndingDate = coupon.EndingDate;
                singleCoupon.RetailID = coupon.RetailID;
                singleCoupon.StartDate = coupon.StartDate;
                singleCoupon.Title = coupon.Title;
                singleCoupon.RetailName = db.Retails.Where(p => p.RetailID == singleCoupon.RetailID).Select(p => p).FirstOrDefault().Name;
                couponsFinal.Add(singleCoupon);
            }
            return couponsFinal.AsQueryable();
        }

        // GET: api/Coupons/5
        [ResponseType(typeof(Coupon))]
        public IHttpActionResult GetCoupon(int id)
        {
            Coupon coupon = db.Coupons.Find(id);
            if (coupon == null)
            {
                return NotFound();
            }

            return Ok(coupon);
        }

        // PUT: api/Coupons/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCoupon(int id, Coupon coupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coupon.CouponID)
            {
                return BadRequest();
            }

            db.Entry(coupon).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CouponExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Coupons
        [ResponseType(typeof(Coupon))]
        public IHttpActionResult PostCoupon(Coupon coupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Coupons.Add(coupon);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = coupon.CouponID }, coupon);
        }

        // DELETE: api/Coupons/5
        [ResponseType(typeof(Coupon))]
        public IHttpActionResult DeleteCoupon(int id)
        {
            Coupon coupon = db.Coupons.Find(id);
            if (coupon == null)
            {
                return NotFound();
            }

            db.Coupons.Remove(coupon);
            db.SaveChanges();

            return Ok(coupon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CouponExists(int id)
        {
            return db.Coupons.Count(e => e.CouponID == id) > 0;
        }
    }
}