# Summary

Using [a Kaggle dataset](https://www.kaggle.com/datasets/ksabishek/massive-bank-dataset-1-million-rows/data) with a max size Excel file containing mock transaction data to perform some aggregation logic in C#.

Planning to utilize the OpenXML library to parse the Excel file and provide multiple solutions to the aggregation questions, comparing performance.

# Questions

1. What is the average transaction value everyday for each domain over the year.
1. What is the average transaction value for every city/location over the year
1. The bank CEO , Mr: Hariharan , wants to promote the ease of transaction for the highest active domain. If the domains could be sorted into a priority, what would be the priority list ?
1. What's the average transaction count for each city ?

# Plan

## First version:

Read the SheetData into an object in memory, iterate over the rows, and update dictionaries to help calculate the answers to the above. More specifically:

- (Date, Domain) -> (SumValue, CountValue)
- (Date, Location) -> (SumValue, CountValue)
- Location -> (SumValue, CountValue)
- Domain -> (SumValue, CountValue)

Once I populate those dicts with the SheetData, I can go through and use the result to calculate the averages.

## Second version:

Read the SheetData as a stream instead.
