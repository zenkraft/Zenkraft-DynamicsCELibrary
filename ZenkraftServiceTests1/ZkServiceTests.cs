using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZenkraftService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenkraftService.Models;

namespace ZenkraftService.Tests
{
    [TestClass()]
    public class ZkServiceTests
    {
        const string api_key = "YETwkOdiQSnfySL5iVCpN83zVZUVDDVyBkk9YCMmiykXA3ouEM";
        const string master_api_key = "VQezfL$nC$8aNH2MFmQ!UHJT1l!e5yzkJZxWylpM0wMiGiyAO77|IAP41wYFW|coXd8Vo4";
        const Int64 fedexShippingAccountId = 5145285479104512; //6502230618275840;
        ZkService zkService = new ZkService(api_key);
        ZkService zkMetadataService = new ZkService(master_api_key);


        [TestMethod()]
        public void GetRatesTest()
        {
            zkService.Test = true;

            Shipment shipment = new Shipment();
            shipment.carrier = "fedex";
            shipment.type = "outbound";
            shipment.dim_units = "CM";
            shipment.weight_units = "KG";
            shipment.currency = "USD";
            shipment.shipping_account = fedexShippingAccountId;

            Address sender = new Address();
            sender.street1 = "10 FED EX PKWY";
            sender.city = "COLLIERVILLE";
            sender.state = "TN";
            sender.postal_code = "38017";
            sender.country = "US";

            shipment.sender = sender;

            Address recipient = new Address();
            recipient.street1 = "525 S. Lexington Ave";
            recipient.city = "Burlington";
            recipient.state = "NC";
            recipient.postal_code = "27215";
            recipient.country = "US";

            shipment.recipient = recipient;

            Package package1 = new Package();
            package1.weight = 33;
            package1.value = 4;
            package1.length = 4;
            package1.width = 4;
            package1.height = 4;

            Package package2 = new Package();
            package2.weight = 33;
            package2.value = 4;
            package2.length = 4;
            package2.width = 4;
            package2.height = 4;

            List<Package> packages = new List<Package>() { package1, package2 };

            shipment.packages = packages;


            string ratesResponse = zkService.GetRates(shipment);

            Assert.IsTrue(!string.IsNullOrEmpty(ratesResponse));
        }



        [TestMethod()]
        public void GetCarriersTest()
        {

            string carriers = zkMetadataService.GetCarriers();
            Assert.IsTrue(!string.IsNullOrEmpty(carriers));
        }

        [TestMethod()]
        public void GetCarrierDetailsTest()
        {
            CarrierDetail fedexDetails = zkMetadataService.GetCarrierDetails("fedex");
            Assert.IsTrue(fedexDetails != null);
        }

        [TestMethod()]
        public void AddCarrierAccountTest()
        {
            string carrierJson = "{\r\n  \"shipping_account\": {\r\n    \"name\": \"My FedEx test account\",\r\n    \"carrier\": \"fedex\",\r\n    \"country\": \"US\",\r\n    \"auth\": {\r\n\t    \"account_number\": \"630156343\",\r\n\t    \"meter_number\": \"119147957\",\r\n\t    \"key\": \"SH2p9UyUWgbhdeAg\",\r\n\t    \"password\": \"v6S5YjRhLEvSTdSwfjOY2uzyW\"\r\n    }\r\n  }\r\n}";
            CarrierAccount carrierAccount = zkService.AddCarrierAccount(carrierJson);

            Assert.IsTrue(carrierAccount != null);
        }

        [TestMethod()]
        public void TrackShipmentTest()
        {
            string trackingNumber = "122816215025810";
            CarrierAccount fedexCarrierAccount = new CarrierAccount();
            fedexCarrierAccount.id = fedexShippingAccountId;
            fedexCarrierAccount.carrier = "fedex";

            zkService.Test = true;
            ShipmentStatus shipmentStatus = zkService.TrackShipment(trackingNumber, fedexCarrierAccount);
            Assert.IsTrue(shipmentStatus != null);
        }

        [TestMethod()]
        public void CancelTest()
        {
            string trackingNumber = "794626099895";
            CarrierAccount fedexCarrierAccount = new CarrierAccount();
            fedexCarrierAccount.id = fedexShippingAccountId;
            fedexCarrierAccount.carrier = "fedex";

            zkService.Test = true;
            ShipmentCancellationResponse cancellationResponse = zkService.Cancel(trackingNumber, fedexCarrierAccount);
            Assert.IsTrue(cancellationResponse != null);
        }

        [TestMethod()]
        public void PickupTest()
        {
            ZkTime zkTime = new ZkTime();
            zkTime.date = "2019-07-18";
            zkTime.ready_time = "15:00";
            zkTime.close_time = "18:00";

            Address pickupLocation = new Address();
            pickupLocation.street1 = "207 Continental Drive";
            pickupLocation.city = "Newark";
            pickupLocation.state = "DE";
            pickupLocation.postal_code = "19720";
            pickupLocation.country = "US";
            pickupLocation.email = "tstemail@tst.com";
            pickupLocation.phone = "293843995";
            pickupLocation.name = "Bob Jones";
            pickupLocation.company = "Tesla Inc";
            pickupLocation.residential = false;
            pickupLocation.location_type = "front";


            Shipment shipment = new Shipment();

          

            Address recipient = new Address();
            recipient.street1 = "525 S. Lexington Ave";
            recipient.city = "Burlington";
            recipient.state = "NC";
            recipient.postal_code = "27215";
            recipient.country = "US";
            recipient.company = "Zk recipient";
            recipient.phone = "1234567898";
            recipient.name = "John Doe";
            recipient.email = "jd@jdtest.com";
           

            shipment.recipient = recipient;

            Package package1 = new Package();
            package1.weight = 33;
            package1.value = 4;
            package1.length = 4;
            package1.width = 4;
            package1.height = 4;

            shipment.packages = new List<Package>() { package1 };


            Pickup pickupTest = new Pickup();
            pickupTest.shipments = new List<Shipment>() { shipment };
            pickupTest.carrier = "fedex";
            pickupTest.shipping_account = fedexShippingAccountId;
            pickupTest.time = zkTime;
            pickupTest.location = pickupLocation;
            pickupTest.test = true;
            pickupTest.debug = true;
            pickupTest.dim_units = "IN";
            pickupTest.weight_units = "LB";
            pickupTest.description = "TEST ENVIRONMENT -PLEASE DO NOT PROCESS PICKUP";



            List<Int64> shipments = new List<Int64>() { 0909090909090, 090909090910 };
       

            zkService.Test = true;
            Pickup pickupResponse = zkService.Pickup(pickupTest);

            Assert.IsTrue(pickupResponse != null);

        }

        [TestMethod()]
        public void ShipTest()
        {
            zkService.Test = true;

            Shipment shipment = new Shipment();
            shipment.carrier = "fedex";
            shipment.type = "outbound";
            shipment.dim_units = "CM";
            shipment.weight_units = "KG";
            shipment.currency = "USD";
            shipment.shipping_account = fedexShippingAccountId;
            shipment.service = "fedex_ground";
            shipment.ship_date = new DateTime(2019, 7, 20);
            shipment.label_type = "PDF";
            shipment.packaging = "your_packaging";

            //shipment.additional_fields = "{\r\n\"testfedex\": true,\r\n\"acc_num\": \"23FD\"\r\n}";



            Address sender = new Address();
            sender.street1 = "10 FED EX PKWY";
            sender.city = "COLLIERVILLE";
            sender.state = "TN";
            sender.postal_code = "38017";
            sender.country = "US";
            sender.company = "Zk sender";
            sender.phone = "1234567890";
            sender.name = "Zack King";
            sender.email = "zk@zktest.com";


            shipment.sender = sender;

            Address recipient = new Address();
            recipient.street1 = "525 S. Lexington Ave";
            recipient.city = "Burlington";
            recipient.state = "NC";
            recipient.postal_code = "27215";
            recipient.country = "US";
            recipient.company = "Zk recipient";
            recipient.phone = "1234567898";
            recipient.name = "John Doe";
            recipient.email = "jd@jdtest.com";


            shipment.recipient = recipient;

            Package package1 = new Package();
            package1.weight = 33;
            package1.value = 4;
            package1.length = 4;
            package1.width = 4;
            package1.height = 4;
            package1.description = "Electronics";
            //package1.carrier_specific = "[\r\n{\r\n  \"productid\" : \"123456\"\r\n}\r\n]";

            Package package2 = new Package();
            package2.weight = 33;
            package2.value = 4;
            package2.length = 4;
            package2.width = 4;
            package2.height = 4;
            package2.description = "Electronics";

            List<Package> packages = new List<Package>() { package1 };

            ZkField reference1 = new ZkField();
            reference1.type = "invoice_number";
            reference1.value = "INV-12345";
            List<ZkField> references = new List<ZkField>() { reference1 };

            shipment.packages = packages;
            shipment.references = references;


            Shipment submittedShipment = zkService.Ship(shipment);

            Assert.IsTrue(submittedShipment != null);
        }

       
    }
}