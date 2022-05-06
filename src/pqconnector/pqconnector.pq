// This file contains your Data Connector logic
section pqconnector;

[DataSource.Kind="pqconnector", Publish="pqconnector.Publish"]
shared pqconnector.Feed = () =>
    let
        source = OData.Feed("https://localhost:7004/odata", null, [ ODataVersion = 4, MoreColumns = true, Implementation="2.0" ])
    in
        source;


// Data Source Kind description
pqconnector = [
    TestConnection = (dataSourcePath) => { "pqconnector.Feed" },
    Authentication = [
        OAuth = [
            StartLogin=StartLogin,
            FinishLogin=FinishLogin,
            Refresh=Refresh,
            Logout=Logout
        ]
    ],
    Label = "ISV Data Platform Sample - OData"
];

// Data Source UI publishing description
pqconnector.Publish = [
    Beta = true,
    Category = "Other",
    ButtonText = { "ISV Demo Power Query Connector", "ISV Demo Power Query Connector" }
];


//oAuth components
//see https://github.com/microsoft/DataConnectors/blob/master/samples/MyGraph/MyGraph.pq

client_id     = Text.FromBinary(Extension.Contents("client_id")); 
redirect_uri  = "https://login.microsoftonline.com/common/oauth2/nativeclient";
token_uri     = "https://login.microsoftonline.com/organizations/oauth2/v2.0/token";
authorize_uri = "https://login.microsoftonline.com/organizations/oauth2/v2.0/authorize";
logout_uri    = "https://login.microsoftonline.com/logout.srf";

scope_prefix = "api://isv-data-platform/";
scopes = {
    "User.Read"
};


StartLogin = (resourceUrl, state, display) =>
    let
        authorizeUrl = authorize_uri & "?" & Uri.BuildQueryString([
            client_id = client_id,  
            redirect_uri = redirect_uri,
            state = state,
            scope = "offline_access " & GetScopeString(scopes, scope_prefix),
            response_type = "code",
            response_mode = "query",
            login = "login"
        ])
    in
        [
            LoginUri = authorizeUrl,
            CallbackUri = redirect_uri,
            WindowHeight = 720,
            WindowWidth = 1024,
            Context = null
        ];


FinishLogin = (context, callbackUri, state) =>
    let
        parts = Uri.Parts(callbackUri)[Query],
        result = if (Record.HasFields(parts, {"error", "error_description"})) then 
                    error Error.Record(parts[error], parts[error_description], parts)
                 else
                    TokenMethod("authorization_code", "code", parts[code])
    in
        result;


Refresh = (resourceUrl, refresh_token) => TokenMethod("refresh_token", "refresh_token", refresh_token);
Logout = (token) => logout_uri;

TokenMethod = (grantType, tokenField, code) =>
    let
        queryString = [
            client_id = client_id,
            scope = "offline_access api://isv-data-platform/access_as_a_user",
            grant_type = grantType,
            redirect_uri = redirect_uri
        ],
        queryWithCode = Record.AddField(queryString, tokenField, code),

        tokenResponse = Web.Contents(token_uri, [
            Content = Text.ToBinary(Uri.BuildQueryString(queryWithCode)),
            Headers = [
                #"Content-type" = "application/x-www-form-urlencoded",
                #"Accept" = "application/json"
            ],
            ManualStatusHandling = {400} 
        ]),
        body = Json.Document(tokenResponse),
        result = if (Record.HasFields(body, {"error", "error_description"})) then 
                    error Error.Record(body[error], body[error_description], body)
                 else
                    body
    in
        result;


// Helper Functions

Value.IfNull = (a, b) => if a <> null then a else b;

GetScopeString = (scopes as list, optional scopePrefix as text) as text =>
    let
        prefix = Value.IfNull(scopePrefix, ""),
        addPrefix = List.Transform(scopes, each prefix & _),
        asText = Text.Combine(addPrefix, " ")
    in
        asText;


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