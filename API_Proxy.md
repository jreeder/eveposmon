# Introduction #

We're releasing a set of php pages that you can run on your local server that will act as a proxy to the eve server.  This will allow you to download the latest starbase details without giving eveposmon your full api key.  You can rest assured that it is safe.

# Explanation #

The way this works is a couple of php files get installed on your server along with a quick mod-rewrite .htacess file.

The incoming request from eveposmon will goto your server.  From there your server will take the request attach your api keys, and then return back to the program just the xml file.  This way the program never receives any api keys so you can feel safe.

## Code ##
```
Sending:
EVEPOSMon --- Request ---> Your Server ---- Request + Keys ----> api.eve-online.com
Recieving:
api.eve-online.com ---XML----> Your Server ---- XML ---> EVEPOSMon
```

The zip file that is available contains 4 files.

```
.htaccess
api/api.php
api/characters.php
api/starbasedetail.php
api/starbaselist.php
```

The .htacess must go in the root directory, if there is already one there, append the information in this one.  You then need to create an api directory and place the 4 files in that directory.

## Editing the Code ##

You will need to edit each file and edit the following lines:
```
//EDIT BELOW THIS LINE
$fullapi->setup("","");
$t = $fullapi->isCharInAPI("");
//EDIT ABOVE THIS LINE
```

During the setup funciton please type in the userId and FULL API keys found on the EVE Api website.  On the isCharInAPI line please type in the character name that will be used that is on that account.  For example yours might look like this:

```
//EDIT BELOW THIS LINE
$fullapi->setup("213563","dsgsd9fgd9bd76f9b678d6bdsf80b6dsfb06");
$t = $fullapi->isCharInAPI("TestChar");
//EDIT ABOVE THIS LINE
```