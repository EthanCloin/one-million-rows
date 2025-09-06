Using `record` for entities i want to easily compare by value eg DateDomain + DateLocation.

First row of the XML parsed Excel is weird and wacky, it is of type `CellValues.SharedString` not just a string value. Don't really need to interact w that since my headers are static for this problem but worth noting.

Trying to directly format the parsed excel 'Date' column to DateTime in my ValueFormatter is not working. Need to check the docs on how to handle dates.

> I need to use the `FromOADate` for excel formatted dates

Also some weirdness with SharedString datatype. I guess that's some optimization that Excel does? But if I identify that a type is shared string, i have to do a lookup to that table, can't just read raw text. The actual XML content is an integer which points to a value in that table. Noted.

Had to be careful with the calculated Value property on my Average class. It was updating every time I changed the underlying properties, which defeats the purpose of storing the total+count instead of just a single average. I only want to do that math once at the end, not on every row.
