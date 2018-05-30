using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using System.Xml.Schema;

namespace DeltaxUnitTest
{
    [TestClass]
    public class UnitTestDemo
    {

        private string xmlString = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
"<Orders pages=\"1\">" + @"
  <Order>
    <OrderID><![CDATA[123456]]></OrderID>
    <OrderNumber><![CDATA[ABC123]]></OrderNumber>
    <OrderDate>12/8/2011 21:56 PM</OrderDate>
    <OrderStatus><![CDATA[paid]]></OrderStatus>
    <LastModified>12/8/2011 12:56 PM</LastModified>
    <ShippingMethod><![CDATA[USPSPriorityMail]]></ShippingMethod>
    <PaymentMethod><![CDATA[Credit Card]]></PaymentMethod>
    <OrderTotal>123.45</OrderTotal>
    <TaxAmount>0.00</TaxAmount>
    <ShippingAmount>4.50</ShippingAmount>
    <CustomerNotes><![CDATA[Please make sure it gets here by Dec. 22nd!]]></CustomerNotes>
    <InternalNotes><![CDATA[Ship by December 18th via Priority Mail.]]></InternalNotes>
    <Gift>false</Gift>
    <GiftMessage></GiftMessage>
    <CustomField1></CustomField1>
    <CustomField2></CustomField2>
    <CustomField3></CustomField3>
    <Customer>
      <CustomerCode><![CDATA[customer@mystore.com]]></CustomerCode>
      <BillTo>
        <Name><![CDATA[The President]]></Name>
        <Company><![CDATA[US Govt]]></Company>
        <Phone><![CDATA[512-555-5555]]></Phone>
        <Email><![CDATA[customer@mystore.com]]></Email>
      </BillTo>
      <ShipTo>
        <Name><![CDATA[The President]]></Name>
        <Company><![CDATA[US Govt]]></Company>
        <Address1><![CDATA[1600 Pennsylvania Ave]]></Address1>
        <Address2></Address2>
        <City><![CDATA[Washington]]></City>
        <State><![CDATA[DC]]></State>
        <PostalCode><![CDATA[20500]]></PostalCode>
        <Country><![CDATA[US]]></Country>
        <Phone><![CDATA[512-555-5555]]></Phone>
      </ShipTo>
    </Customer>
    <Items>
      <Item>
        <SKU><![CDATA[FD88821]]></SKU>
        <Name><![CDATA[My Product Name]]></Name>
        <ImageUrl><![CDATA[http://www.mystore.com/products/12345.jpg]]></ImageUrl>
        <Weight>8</Weight>
        <WeightUnits>Ounces</WeightUnits>
        <Quantity>2</Quantity>
        <UnitPrice>13.99</UnitPrice>
        <Location><![CDATA[A1-B2]]></Location>
        <Options>
          <Option>
            <Name><![CDATA[Size]]></Name>
            <Value><![CDATA[Large]]></Value>
            <Weight>10</Weight>
          </Option>
          <Option>
            <Name><![CDATA[Color]]></Name>
            <Value><![CDATA[Green]]></Value>
            <Weight>5</Weight>
          </Option>
        </Options>
      </Item>
      <Item>
        <SKU></SKU>
        <Name><![CDATA[$10 OFF]]></Name>
        <Quantity>1</Quantity>
        <UnitPrice>-10.00</UnitPrice>
        <Adjustment>true</Adjustment>
      </Item>
    </Items>
  </Order>
</Orders>";

        public string ToXml(object source)
        {
            string str;
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Encoding = new UnicodeEncoding(false, false),
                Indent = false,
                OmitXmlDeclaration = true
            };
            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    xmlSerializer.Serialize(xmlWriter, source, namespaces);
                }
                str = stringWriter.ToString();
            }
            return str;
        }

        [TestMethod]
        public void TestMethod1()
        {
            OrdersResponseDTO dto = new OrdersResponseDTO()
            {
                Page = 1,
                Orders = new List<OrderDTO>()
                {
                    new OrderDTO() {
                        OrderID= 123456.ToString().ToCData() ,
                        OrderNumber="ABC123".ToCData(),
                        OrderDate=DateTime.Now,
                        OrderStatus="paid".ToCData(),
                        LastModified=DateTime.Now,
                        ShippingMethod ="USPSPriorityMail",
                        PaymentMethod ="Credit Card",
                        OrderTotal=123.45 ,
                        TaxAmount = 0.00 ,
                        ShippingAmount = 4.50 ,
                        CustomerNotes= "Please make sure it gets here by Dec. 22nd!",
                        InternalNotes="Ship by December 18th via Priority Mail.",
                        Gift = false,
                        GiftMessage ="",
                        CustomField1 = "",
                        CustomField2 = "",
                        CustomField3 = "",
                        Customer=new Customer() {
                            CustomerCode="customer@mystore.com",
                            BillTo=new BillTo() {
                                Name="The President",
                                Company="US Govt",
                                Phone="512-555-5555",
                                Email="customer@mystore.com"
                            },
                            ShipTo=new ShipTo() {
                                 Name="The President",
                                 Company="US Govt",
                                 Address1="1600 Pennsylvania Ave",
                                 Address2="",
                                 City="Washington",
                                 State="DC",
                                 PostalCode="20500",
                                 Country="US",
                                 Phone="512-555-5555"
                            }
                        },
                        Items=new List<PurchesItem>() {
                            new PurchesItem() {
                                 SKU="FD88821",
                                 Name="My Product Name",
                                 ImageUrl="http://www.mystore.com/products/12345.jpg",
                                 Weight=8,
                                 WeightUnits="Ounces",
                                 Quantity= 2,
                                 UnitPrice=13.99,
                                 Location="A1-B2",
                                 Options=new List<Option>() {
                                     new Option() {
                                         Name="Size",
                                         Value="Large",
                                         Weight=10
                                     },
                                     new Option() {
                                          Name="Color",
                                         Value="Green",
                                         Weight=5
                                     },
                                 }
                            },
                            new PurchesItem() {
                                 SKU="",
                                 Name="$10 OFF",
                                 Quantity=1,
                                 UnitPrice=-10.00,
                                 Adjustment= true
                            }
                        }
                    }
                }
            };

            string xmldata = ToXml(dto);
            XmlSerializer serializer = new XmlSerializer(typeof(OrdersResponseDTO));


            using (StringReader reader = new StringReader(xmlString))
            {
                OrdersResponseDTO book = (OrdersResponseDTO)(serializer.Deserialize(reader));
            }
        }
    }
}

public static class Extension
{
    public static CDATA ToCData(this string source)
    {
        return new CDATA(source);
    }
}
public class CDATA : IXmlSerializable
{

    private string text;

    public CDATA()
    { }

    public CDATA(string text)
    {
        this.text = text;
    }

    public string Text
    {
        get { return text; }
    }

    XmlSchema IXmlSerializable.GetSchema()
    {
        return null;
    }

    void IXmlSerializable.ReadXml(XmlReader reader)
    {
        this.text = reader.ReadString();
        reader.Read(); // change in .net 2.0,
                       // without this line, you will lose value of all other fields
    }

    void IXmlSerializable.WriteXml(XmlWriter writer)
    {
        writer.WriteCData(this.text);
    }
    public override string ToString()
    {
        return this.text;
    }
}

[XmlRoot("Orders")]
public class OrdersResponseDTO
{
    [XmlAttribute("pages")]
    public int Page { get; set; }

    [XmlElement("Order")]
    public List<OrderDTO> Orders { get; set; }
}
public class OrderDTO
{
    private CDATA _OrderID;
    [XmlElement("OrderID", typeof(CDATA))]
    public CDATA OrderID
    {
        get { return _OrderID; }
        set { _OrderID = value; }
    }

    private CDATA _OrderNumber;
    [XmlElement("OrderNumber", typeof(CDATA))]
    public CDATA OrderNumber
    {
        get { return _OrderNumber; }
        set { _OrderNumber = value; }
    }

    public DateTime OrderDate { get; set; }

    private CDATA _OrderStatus;
    [XmlElement("OrderStatus", typeof(CDATA))]
    public CDATA OrderStatus
    {
        get { return _OrderStatus; }
        set { _OrderStatus = value; }
    }
    public DateTime LastModified { get; set; }
    public string ShippingMethod { get; set; }
    public string PaymentMethod { get; set; }
    public double OrderTotal { get; set; }
    public double TaxAmount { get; set; }
    public double ShippingAmount { get; set; }
    public string CustomerNotes { get; set; }
    public string InternalNotes { get; set; }
    public bool Gift { get; set; }
    public string GiftMessage { get; set; }
    public string CustomField1 { get; set; }
    public string CustomField2 { get; set; }
    public string CustomField3 { get; set; }

    public Customer Customer { get; set; }

    [XmlArray("Item")]
    public List<PurchesItem> Items { get; set; }
}

public class Customer
{
    public string CustomerCode { get; set; }
    public BillTo BillTo { get; set; }
    public ShipTo ShipTo { get; set; }
}

public class BillTo
{
    public string Name { get; set; }
    public string Company { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}

public class ShipTo
{
    public string Name { get; set; }
    public string Company { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
}

public class PurchesItem
{
    public string SKU { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public double Weight { get; set; }
    public string WeightUnits { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public string Location { get; set; }
    public bool Adjustment { get; set; }
    [XmlArray("Option")]
    public List<Option> Options { get; set; }
}

public class Option
{
    public string Name { get; set; }
    public string Value { get; set; }
    public double Weight { get; set; }
}