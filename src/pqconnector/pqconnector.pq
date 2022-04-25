// This file contains your Data Connector logic
section pqconnector;

//https://localhost:7004/Orders


[DataSource.Kind="pqconnector", Publish="pqconnector.Publish"]
shared pqconnector.Feed = Value.ReplaceType(pqconnectorImpl, type function (url as Uri.Type) as any);

pqconnectorImpl = (url as text) =>
    let
        source = OData.Feed(url)
    in
        source;

// Data Source Kind description
pqconnector = [
    Authentication = [
        Anonymous = []
    ],
    Label = "TripPin Part 1 - OData"
];

// Data Source UI publishing description
pqconnector.Publish = [
    Beta = true,
    Category = "Other",
    ButtonText = { "ISV Demo Power Query Connector", "ISV Demo Power Query Connector" }
];

// Data Source UI publishing description
// pqconnector.Publish = [
//     Beta = true,
//     Category = "Other",
//     ButtonText = { Extension.LoadString("ButtonTitle"), Extension.LoadString("ButtonHelp") },
//     LearnMoreUrl = "https://powerbi.microsoft.com/",
//     SourceImage = pqconnector.Icons,
//     SourceTypeImage = pqconnector.Icons
// ];
// 
// pqconnector.Icons = [
//     Icon16 = { Extension.Contents("pqconnector16.png"), Extension.Contents("pqconnector20.png"), Extension.Contents("pqconnector24.png"), Extension.Contents("pqconnector32.png") },
//     Icon32 = { Extension.Contents("pqconnector32.png"), Extension.Contents("pqconnector40.png"), Extension.Contents("pqconnector48.png"), Extension.Contents("pqconnector64.png") }
// ];
// 