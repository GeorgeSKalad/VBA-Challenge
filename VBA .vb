Attribute VB_Name = "Module1"
Sub stock_anlysis():
    'Idenify the variables
    Dim Ticker As String
    Dim Opening_Price As Double
    Dim Closing_Price As Double
    Dim Yearly_Change As Double
    Dim Percent_Change As Double
    Dim i As Long
    Dim j As Integer
        
    'cycle formulas throughout the worksheets
    For Each ws In Worksheets

    'Track the ranges throughout the worksheets
    Dim Price_Row As Long
    Price_Row = 2

    'Assign the total stock volume as 0
    Total = 0
    
    'This tracks the location for the stock's ticker name
    Dim_Summary_Table_Row = 2
    Summary_Table_Row = 2
    
    'Set title row headers for the following:
    ws.Range("I1").Value = "Ticker"
    ws.Range("J1").Value = "Yearly Change"
    ws.Range("K1").Value = "Percent Change"
    ws.Range("L1").Value = "Total Stock Volume"
    ws.Range("P1").Value = "Ticker"
    ws.Range("Q1").Value = "Value"
    ws.Range("O2").Value = "Greatest % Increase"
    ws.Range("O3").Value = "Greatest % Decrease"
    ws.Range("O4").Value = "Greatest Total Volume"
    
    'provide the row number for the last row 
    Row_Count = ws.Cells(Rows.Count, 1).End(xlUp).Row
        
            'Loops through column A to find each stock name
            For i = 2 To Row_Count:
                'Use the If condition to compare cells
                If ws.Cells(i + 1, 1).Value <> ws.Cells(i, 1).Value Then
                
                    'Set the ticker
                    Ticker = ws.Cells(i, 1).Value
                    
                    'Add total stock volume &print ticker
                    Total = Total + ws.Range("G" & i).Value
                    ws.Range("I" & Summary_Table_Row).Value = Ticker
                    
                    'prints the total stock volume
                    ws.Range("L" & Summary_Table_Row).Value = Total
                    
                    'calculate yearly change &percent change
                    Opening_Price = ws.Range("C" & Price_Row).Value
                    Closing_Price = ws.Range("F" & i).Value
                    Yearly_Change = Closing_Price - Opening_Price
                    
                    'handles zero
                        If Opening_Price = 0 Then
                            Percent_Change = 0
                            Else
                                Percent_Change = Yearly_Change / Opening_Price
                            End If
                            
                        'prints the values of yearly change & percent change
                        ws.Range("J" & Summary_Table_Row).Value = Yearly_Change
                        ws.Range("K" & Summary_Table_Row).Value = Percent_Change
                        ws.Range("K" & Summary_Table_Row).NumberFormat = "0.00%"
                            
                    'conditional formatting to display positive changes in "green" and negative in "red"
                    If ws.Range("J" & Summary_Table_Row).Value > 0 Then
                        ws.Range("J" & Summary_Table_Row).Interior.ColorIndex = 4
                    Else
                        ws.Range("J" & Summary_Table_Row).Interior.ColorIndex = 3
                    End If
                    
                    'adds one to the summary table row
                    Summary_Table_Row = Summary_Table_Row + 1
                    Price_Row = i + 1
                    
                    'reset the total stock volume
                    Total = 0
                    
                Else
                    Total = Total + ws.Range("G" & i).Value
                End If
                
            Next i
            
        'stock percentage change as well as the stock volume
        Greatest_Percent_Increase = ws.Range("K2").Value
        Greatest_Percent_Decrease = ws.Range("K2").Value
        Greatest_Percent_Total = ws.Range("L2").Value
        
        'last row of Ticker column
        Row_Count = ws.Cells(Rows.Count, "I").End(xlUp).Row
        
        'loop through ticker column row to find the highest 
        For j = 2 To Row_Count:
            If ws.Range("K" & j + 1).Value > Greatest_Percent_Increase Then
                Greatest_Percent_Increase = ws.Range("K" & j + 1).Value
                Greatest_Perc_Increase_Ticker = ws.Range("I" & j + 1).Value
            ElseIf ws.Range("K" & j + 1).Value < Greatest_Percent_Decrease Then
                Greatest_Percent_Decrease = ws.Range("K" & j + 1).Value
                Greatest_Perc_Decrease_Ticker = ws.Range("I" & j + 1).Value
            ElseIf ws.Range("L" & j + 1).Value > Greatest_Percent_Total Then
                Greatest_Percent_Total = ws.Range("L" & j + 1).Value
                Greatest_Total_Ticker = ws.Range("I" & j + 1).Value
            End If
        Next j
        
        'prints the greatest % increase, greatest % decrease, greatest total volume, and the associated stock
        ws.Range("P2").Value = Greatest_Perc_Increase_Ticker
        ws.Range("P3").Value = Greatest_Perc_Decrease_Ticker
        ws.Range("P4").Value = Greatest_Total_Ticker
        ws.Range("Q2").Value = Greatest_Percent_Increase
        ws.Range("Q3").Value = Greatest_Percent_Decrease
        ws.Range("Q4").Value = Greatest_Percent_Total
        ws.Range("Q2:Q3").NumberFormat = "0.00%"
    Next ws
    
End Sub