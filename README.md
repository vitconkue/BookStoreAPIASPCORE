# BookStoreAPIASPCORE
```
/api/book 
METHOD: GET
Result: all books in database
```

``` 
api/book
METHOD: POST
Format: JSON
{
  "Title": "title here",
  "Author": "Author here",
  "TypeID": type (integer),
  "Amount": amount (interger)

}
Result: Error if failed, whole book information if succeeded
