#!/bin/bash
mongo --username admin --password pass <<EOF
use books-api
db.createCollection('Books',
{
  autoIndexId: true
});
EOF