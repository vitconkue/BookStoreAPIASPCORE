# BookStoreAPIASPCORE
Get All Books
```
/api/book 
METHOD: GET
Result: all books in database
```
Add book
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

```

Get one book
```
api/book/{id: integer}
METHOD: GET
Result: Error if failed, whole book information if succeeded

```
Update an existed book
```
api/book/update
METHOD: POST
Format: JSON
{
  "Id": Book Id to update here (integer),
  "Title": "new title here",
  "Author": "new Author here",
  "TypeID": new type (integer)",
  "Amount": amount (interger)

}
Result: 404 if not found
Note: Not updated information => stil keep old value in sending JSON, don't leave it blank
```

Delete an existed book
```
api/book/delete/{id: integer}
METHOD: GET
Result: result of the deleting method, 404 if not found

```

----------------------------------------------------------------------------------------------------------
Configurations: 4 types
```
MaximumAmountBookLeftBeforeImport: chi nhap cac loai sach co luong ton it hon xxx
```
```
MinimumImportBook: So luong nhap toi da mot luc
```
```
MaximumDebtCustomer: Neu no hon xxx thi khong lap hoa don cho khach
```
```
MaximumAmountBookLeftAfterSelling: Sau khi ban xong phai con lai it nhat xxx
```


Get All Configuration
```
api/configurations
METHOD: GET
Result: All configurations | not found
```

Get 1 Configuration
```
api/configurations/{name}
METHOD: GET
Result: 1 configuration | not found
```

Toogle Configuration
```
api/configurations/toggle/{name}
METHOD: POST
Result: if success: new configuration object | if failed: not found

```

