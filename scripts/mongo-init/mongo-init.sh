#!/bin/bash
mongo --username admin --password pass <<EOF
use books-api
db.createCollection('Books',
{
  autoIndexId: true
});
db.Books.insertMany([
  {
    _id: 1,
    title: 'Winnie-the-pooh',
    author: 'A. A. Milne',
    price: 19.25
  },
  {
    _id: 2,
    title: 'Pride and Prejudice',
    author: 'Jane Austin',
    price: 5.49
  },
  {
    _id: 3,
    title: 'Romeo and Juliet',
    author: 'William Shakespeare',
    price: 6.95
  }
])
EOF
