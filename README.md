# Holy Quran 

Download Neo4j
https://neo4j.com/

Run Neo4j 

Open http://localhost:7474

Set:
Username:neo4j
Password:123456

Download the Source code

Open Solution in Visual Studio 2015

Install-Package Neo4jClient

Run Program

Or 

Run  QuranToNeo4j.exe in the bin folder 
https://github.com/kassemshehady/holyquran/blob/master/QuranToNeo4j/QuranToNeo4j/bin/Debug/QuranToNeo4j.exe


Done!

# How it works

The system links every word to the next word that occures in the same AYA

# Top Words 

MATCH (w:Word)-[r:NEXT]->() RETURN w.title as t ,Count(r) as c order by c desc LIMIT 1000

من	834

الله	777

ما	442

ولا	418

أن	401

لا	386

في	360

إن	267

وما	250

إلا	247

على	234

ثم	213

أو	211

لهم	205




Text Source by http://tanzil.ca/download/
