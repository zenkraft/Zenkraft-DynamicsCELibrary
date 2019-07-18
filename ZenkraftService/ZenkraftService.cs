using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenkraftService.Models;

namespace ZenkraftService
{
    public class ZkService
    {
        public string ApiKey { get; set; }
        public DataLayer DataLayer  = new DataLayer();
        public bool Test = false;

        public ZkService(string apiKey)
        {
            ApiKey = apiKey;
        }

        public string GetRates(Shipment shipment)
        {
            string ratesResponse = string.Empty;

            try
            {
                dynamic dataObject = new ExpandoObject();

                //Shipment details


                dynamic shipmentObj = new ExpandoObject();
                shipmentObj.carrier = shipment.carrier;
                shipmentObj.type = shipment.type;
                shipmentObj.test = Test;
                shipmentObj.dim_units = shipment.dim_units;
                shipmentObj.weight_units = shipment.weight_units;
                shipmentObj.currency = shipment.currency;
                shipmentObj.shipping_account = shipment.shipping_account;

                //sender
                Address sender = shipment.sender;

                dynamic senderObj = new ExpandoObject();
                senderObj.street1 = sender.street1;
                senderObj.city = sender.city;
                senderObj.state = sender.state;
                senderObj.postal_code = sender.postal_code;
                senderObj.country = sender.country;

                //recipient
                Address recipient = shipment.recipient;

                dynamic recipientObj = new ExpandoObject();
                recipientObj.street1 = recipient.street1;
                recipientObj.city = recipient.city;
                recipientObj.state = recipient.state;
                recipientObj.postal_code = recipient.postal_code;
                recipientObj.country = recipient.country;

                JArray packagesArray = new JArray();
                if (shipment.packages != null)
                {
                    if (shipment.packages.Count == 0)
                    {
                        throw new Exception("No packages were found for this shipment.");

                    }
                    else
                    {
                        foreach (Package packageObj in shipment.packages)
                        {
                            dynamic package = new ExpandoObject();
                            package.weight = packageObj.weight;
                            package.value = packageObj.value;
                            package.length = packageObj.length;
                            package.width = packageObj.width;
                            package.height = packageObj.height;
                            packagesArray.Add(JObject.FromObject(package));

                        }
                    }
                }
                else
                {
                    throw new Exception("No packages were found for this shipment.");
                }

                shipmentObj.sender = senderObj;
                shipmentObj.recipient = recipientObj;
                shipmentObj.packages = packagesArray;

                dataObject.shipment = shipmentObj;

                string jsonObj = JsonConvert.SerializeObject(dataObject);
                ratesResponse = DataLayer.getResponseFromApi(ApiKey, DataLayer.WebMethod.POST, "/rate", jsonObj);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return ratesResponse;
        }

        public string GetCarriers()
        {

            string carriersResult = string.Empty;
            try
            {
                carriersResult = DataLayer.getResponseFromApi(ApiKey, DataLayer.WebMethod.GET, "/meta/carriers");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return carriersResult;
        }

        public CarrierDetail GetCarrierDetails(string carrierName)
        {
            CarrierDetail carrierDetail = null;

            try
            {
                string jsonResponse = DataLayer.getResponseFromApi(ApiKey, DataLayer.WebMethod.GET, $"/meta/carrier/{carrierName}");
                carrierDetail = JsonConvert.DeserializeObject<CarrierDetail>(jsonResponse);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return carrierDetail;

        }

        public CarrierAccount AddCarrierAccount(string carrierJson)
        {

            CarrierAccount carrierAccount = null;

            try
            {
                string jsonResponse = DataLayer.getResponseFromApi(ApiKey, DataLayer.WebMethod.POST, "/shippingaccount", carrierJson);

                if (!string.IsNullOrEmpty(jsonResponse))
                {
                    JObject dataObj = JObject.Parse(jsonResponse);
                    JObject carrierAccountObj = JObject.Parse(dataObj["shipping_account"].ToString());
                    carrierAccount = new CarrierAccount();
                    carrierAccount.auth = carrierAccountObj["auth"].ToString();
                    carrierAccount.name = carrierAccountObj["name"].ToString();
                    carrierAccount.id = Int64.Parse(carrierAccountObj["id"].ToString());
                    carrierAccount.carrier = carrierAccountObj["carrier"].ToString();
                    carrierAccount.dim_units = carrierAccountObj["dim_units"].ToString();
                    carrierAccount.weight_units = carrierAccountObj["weight_units"].ToString();
                    carrierAccount.currency = carrierAccountObj["currency"].ToString();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return carrierAccount;
        }

        public ShipmentStatus TrackShipment(string trackingNumber, CarrierAccount carrierAccount)
        {
            ShipmentStatus shipmentStatus = null;

            try
            {
                dynamic dataObject = new ExpandoObject();
                dynamic track = new ExpandoObject();
                track.shipping_account = carrierAccount.id;
                track.carrier = carrierAccount.carrier;
                track.tracking_number = trackingNumber;
                track.test = Test;

                dataObject.track = track;
                string payload = JsonConvert.SerializeObject(dataObject);
                string jsonResponse = DataLayer.getResponseFromApi(ApiKey, DataLayer.WebMethod.POST, "/track", payload);
                shipmentStatus = JsonConvert.DeserializeObject<ShipmentStatus>(jsonResponse);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return shipmentStatus;
        }

        public ShipmentCancellationResponse Cancel(string trackingNumber, CarrierAccount carrierAccount)
        {
            ShipmentCancellationResponse shipmentCancellationResponse = null;

            try
            {
                dynamic dataObject = new ExpandoObject();
                dynamic cancel = new ExpandoObject();
                cancel.shipping_account = carrierAccount.id;
                cancel.carrier = carrierAccount.carrier;
                cancel.tracking_number = trackingNumber;
                cancel.test = Test;

                dataObject.cancel = cancel;
                string payload = JsonConvert.SerializeObject(dataObject);
                string jsonResponse = DataLayer.getResponseFromApi(ApiKey, DataLayer.WebMethod.POST, "/cancel", payload);
                shipmentCancellationResponse = convertCancellationResponse(jsonResponse);

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return shipmentCancellationResponse;
        }

        public Shipment Ship(Shipment shipment)
        {
            Shipment submittedShipment = null;

            try
            {
                dynamic dataObject = new ExpandoObject();

                //Shipment details


                dynamic shipmentObj = new ExpandoObject();
                shipmentObj.carrier = shipment.carrier;
                shipmentObj.type = shipment.type;
                shipmentObj.test = Test;
                shipmentObj.debug = shipment.debug;
                shipmentObj.dim_units = shipment.dim_units;
                shipmentObj.weight_units = shipment.weight_units;
                shipmentObj.currency = shipment.currency;
                shipmentObj.shipping_account = shipment.shipping_account;
                shipmentObj.service = shipment.service;
                shipmentObj.ship_date = shipment.ship_date.ToString("yyyy-MM-dd");
                shipmentObj.label_type = shipment.label_type;
                shipmentObj.packaging = shipment.packaging;
            
        


                //sender
                Address sender = shipment.sender;

                dynamic senderObj = new ExpandoObject();
                senderObj.street1 = sender.street1;
                senderObj.city = sender.city;
                senderObj.state = sender.state;
                senderObj.postal_code = sender.postal_code;
                senderObj.country = sender.country;
                senderObj.company = sender.company;
                senderObj.phone = sender.phone;
                senderObj.name = sender.name;
                senderObj.email = sender.email;

                //recipient
                Address recipient = shipment.recipient;

                dynamic recipientObj = new ExpandoObject();
                recipientObj.street1 = recipient.street1;
                recipientObj.city = recipient.city;
                recipientObj.state = recipient.state;
                recipientObj.postal_code = recipient.postal_code;
                recipientObj.country = recipient.country;
                recipientObj.company = recipient.company;
                recipientObj.phone = recipient.phone;
                recipientObj.name = recipient.name;
                recipientObj.email = recipient.email;




                JArray packagesArray = new JArray();
                if (shipment.packages != null)
                {
                    if (shipment.packages.Count == 0)
                    {
                        throw new Exception("No packages were found for this shipment.");

                    }
                    else
                    {
                        foreach (Package package in shipment.packages)
                        {
                        

                            dynamic packageObj = new ExpandoObject();
                            packageObj.weight = package.weight;
                            packageObj.value = package.value;
                            packageObj.length = package.length;
                            packageObj.width = package.width;
                            packageObj.height = package.height;
                            //packageObj.description = package.description;
                            packageObj.carrier_specific = new JArray();

                            if(!string.IsNullOrEmpty(package.carrier_specific))
                            {
                                packageObj.carrier_specific = JArray.Parse(package.carrier_specific);
                            }

                            packagesArray.Add(JObject.FromObject(packageObj));

                        }
                    }
                }
                else
                {
                    throw new Exception("No packages were found for this shipment.");
                }

                JArray referencesArray = new JArray();

                if(shipment.references != null)
                {
                    foreach(ZkField reference in shipment.references)
                    {
                        dynamic referenceObj = new ExpandoObject();
                        referenceObj.type = reference.type;
                        referenceObj.value = reference.value;

                        referencesArray.Add(JObject.FromObject(referenceObj));
                    }
                }

                shipmentObj.sender = senderObj;
                shipmentObj.recipient = recipientObj;
                shipmentObj.packages = packagesArray;
                shipmentObj.references = referencesArray;

                JObject shipmentJObj = JObject.FromObject(shipmentObj);

                if (!string.IsNullOrEmpty(shipment.additional_fields))
                {
                    JObject shipmentExtraFields = JObject.Parse(shipment.additional_fields);
                    foreach (var x in shipmentExtraFields.PropertyValues())
                    {
                        string propertyName = x.Path;
                        shipmentJObj[propertyName] = shipmentExtraFields[propertyName];
                    }
                }

                dataObject.shipment = shipmentJObj;

                string jsonObj = JsonConvert.SerializeObject(dataObject);
                string jsonResponse = DataLayer.getResponseFromApi(ApiKey, DataLayer.WebMethod.POST, "/ship", jsonObj);

                JObject responseObj = JObject.Parse(jsonResponse);
                if (responseObj["shipment"] != null)
                {
                    submittedShipment = JsonConvert.DeserializeObject<Shipment>(responseObj["shipment"].ToString());
                }

                if(responseObj["error"] != null)
                {
                    throw new Exception(responseObj["error"]["message"].ToString());
                }


            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return submittedShipment;
        }

       

        public Pickup Pickup(Pickup pickup)
        {
            Pickup pickupResult = null;

            try
            {
                JObject dataObj = new JObject();
                dataObj["pickup"] = JObject.FromObject(pickup);

                List<Shipment> shipments = pickup.shipments;
                JArray shipmentArr = new JArray();
                foreach(Shipment shipment in shipments)
                {
                    dynamic shipmentObj = new ExpandoObject();

                    Address recipient = shipment.recipient;
                    dynamic recipientObj = new ExpandoObject();
                    recipientObj.street1 = recipient.street1;
                    recipientObj.city = recipient.city; 
                    recipientObj.state = recipient.state;
                    recipientObj.postal_code = recipient.postal_code;
                    recipientObj.country = recipient.country; 
                    recipientObj.company = recipient.company; 
                    recipientObj.phone = recipient.phone;
                    recipientObj.name = recipient.name;
                    recipientObj.email = recipient.email;
             

                    shipmentObj.recipient = recipientObj;

                    JArray packagesArr = new JArray();
                    List<Package> packages = shipment.packages;
                    foreach(Package package in packages)
                    {
                        dynamic packageObj = new ExpandoObject();
                        packageObj.weight = (int)package.weight;
                        packageObj.value = (int)package.value;
                        packageObj.length = package.length;
                        packageObj.width = package.width;
                        packageObj.height = package.height;

                        packagesArr.Add(JObject.FromObject(packageObj));
                    }

                    shipmentObj.packages = packagesArr;
                    shipmentArr.Add(JObject.FromObject(shipmentObj));
                }

                dataObj["pickup"]["shipments"] = shipmentArr;






                string jsonObj = JsonConvert.SerializeObject(dataObj);
                string jsonResponse = DataLayer.getResponseFromApi(ApiKey, DataLayer.WebMethod.POST, "/pickup", jsonObj);

                JObject responseObj = JObject.Parse(jsonResponse);
                if(responseObj["error"] == null)
                {
                    pickupResult = JsonConvert.DeserializeObject<Pickup>(responseObj["pickup"].ToString());
                }
                else
                {
                    throw new Exception(responseObj["error"]["detail"].ToString());
                }
                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return pickupResult;
        }


        #region Helpers
        private ShipmentCancellationResponse convertCancellationResponse(string jsonResponse)
        {
            ShipmentCancellationResponse shipmentCancellationResponse = new ShipmentCancellationResponse();
            try
            {
                JObject responseObj = JObject.Parse(jsonResponse);


                if (responseObj["success"] != null)
                {
                    shipmentCancellationResponse.success = true;
                    JObject successObj = JObject.Parse(responseObj["success"].ToString());
                    string successMessage = successObj["message"].ToString();

                    shipmentCancellationResponse.message = successMessage;

                }
                else
                {
                    shipmentCancellationResponse.success = false;
                    string errorMessage = "API response is different than the one expected - " + responseObj.ToString();
                    if (responseObj["error"] != null)
                    {
                        JObject errorObj = JObject.Parse(responseObj["error"].ToString());
                        errorMessage = errorObj["message"].ToString();
                    }

                    shipmentCancellationResponse.message = errorMessage;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return shipmentCancellationResponse;
        }

        #endregion
    }
}
