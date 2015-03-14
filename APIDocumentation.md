# API Documentation #

This document has data gathered from: http://wiki.eve-dev.net/API_Coming_Features

This page also contains documentation on all of the other api sources: http://wiki.eve-dev.net/APIv2_Page_Index

Full database downloads from trinity can be found here: http://myeve.eve-online.com/ingameboard.asp?a=topic&threadID=650828

# Details #

## /corp/StarbaseList.xml.aspx ##
```
<rowset name="starbases" key="itemID" columns="typeID,typeName,itemID,locationID,locationName">
<row typeID="12235" typeName="Amarr Control Tower" itemID="150354725" locationID="30000380" locationName="Polaris"/>
```


## /corp/StarbaseDetail.xml.aspx ##

```
<eveapi version="1">
 <currentTime>2007-08-31 10:18:10</currentTime>
 <result>
   <stateTimestamp>2007-08-14 13:47:02</stateTimestamp>
   <rowset name="fuel" key="typeID" columns="typeID,quantity">
     <row typeID="16272" quantity="60000"/>
     <row typeID="16273" quantity="60000"/>
     <row typeID="24597" quantity="400"/>
     <row typeID="24596" quantity="400"/>
     <row typeID="24595" quantity="400"/>
     <row typeID="24594" quantity="400"/>
     <row typeID="24593" quantity="400"/>
     <row typeID="24592" quantity="400"/>
     <row typeID="16274" quantity="148500"/>
     <row typeID="9848" quantity="330"/>
     <row typeID="9832" quantity="2640"/>
     <row typeID="3689" quantity="1650"/>
     <row typeID="3683" quantity="8250"/>
     <row typeID="44" quantity="1320"/>
   </rowset>
 </result>
 <cachedUntil>2007-08-31 10:18:21</cachedUntil>
</eveapi>
```

# Additional Information #

  * When the tower goes into reinforced, stateTimestamp is set to the time that it will come out, to prevent the server from processing the POS until then --Garthagk

# Q&A #

  * Is it possible to get powergrid/cpu usage for starbases?
> > o Unfortunatly it's not possible to get powergrid/cpu usage for POS, they have to be calculated based on fuel usage between stateTimestamps or prestored in your application. --Garthagk
  * POS structures are handled in the DOGMA server code which is not accessible by the API, Garthagk said those are loaded on server startup into memory and assembled to single POS based on their locations --Exi 22:09, 31 August 2007 (CDT))