# Stech Backend Technical Challenge

Hi, welcome to the tech challenge. We'd like you to create an api and console application in order to consume one api endpoint. Challenge should take 1 day, please don't try to complete all tasks. Designing architecture and your approaches are more important for us.

## The Challenge
- Create CRUD api methods of "Book", "Author" entities and also relationships . (any DTO  tools are accepted) 
Services have to follow REST standarts. These methods also should support OPTIONS, GET, POST, PUT, DELETE - HTTP verb.

- Book entity should have SalesCount column and we need one more API method to increase SalesCount by 1 every calls. 
  
  Example Payload : 
  
  ```{"BookId": 1}```

- Create Console app and make 1000 async calls to each 5 different books to increase SalesCount at the same time (totally 5000 async calls) and report it to screen with book name, sales count, avg response time

  Example Result:
  ```
  Book 1		1000		20ms
  Book 2		1000		12ms
  Book 3		1000		22ms
  Book 4		1000		24ms
  Book 5		1000		45ms
  ```


- Create api project’s dockerfile 

