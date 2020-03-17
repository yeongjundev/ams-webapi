# API Endpoints

### Common Query Options

If specified, following query options are available for endpoint.

#### Search

~/api/resources/?search=
search: keyword to be searched

- Empty string will be ignored.

#### Sort (Ordering)

~/api/resources/?orderBy=&orderByDesc=  
orderBy: property name on which ascending ordering is performed
orderByDesc: property name on which descending ordering is performed

- Empty string will be ignored.
- If not specified, default descending ordering action will be
  performed on `createDateTime` property.

#### Paging

~/api/resources/?pageSize=&currentPage=  
pageSize: the number of items to be shown in a page
currentPage: page number to be shown

- Empty string will be ignored.
- If not specified, default paging action will be performed.
  (pageSize = 30, currentPage = 1)

### Student Resources

#### [GET] ~/api/students/{studentId}

```json
{
  "id": 99,
  "firstname": "Yeongjun",
  "middlename": "",
  "lastname": "Im",
  "email": "yeongjundev@gmail.com",
  "phone": "",
  "description": "Graduate software engineer, love to learn new tech!",
  "createDateTime": "",
  "updateDateTime": "",
  "enrolments": [
    {
      "createDateTime": "",
      "lesson": {
        "id": 12,
        "title": "Beginner Class Adult 3",
        "description": "Beginner class for student over 18.",
        "createDateTime": "",
        "updateDateTime": ""
      }
    }
  ]
}
```

#### [GET] ~/api/students/

:heavy_check_mark:Search  
:heavy_check_mark:Sort  
:heavy_check_mark:Paging

```json
[
  {
    "id": 99,
    "firstname": "Yeongjun",
    "middlename": "",
    "lastname": "Im",
    "email": "yeongjundev@gmail.com",
    "phone": "",
    "description": "Graduate software engineer, love to learn new tech!",
    "createDateTime": "",
    "updateDateTime": ""
  }
]
```
