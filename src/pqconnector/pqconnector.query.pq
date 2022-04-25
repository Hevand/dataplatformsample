// Use this file to write queries to test your data connector
let
    result = pqconnector.Feed("https://localhost:7004/Orders")
in
    result