   M S B A M L   `   `   `  ����N  JZadanieRekrutacyjne, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:ZadanieRekrutacyjneZadanieRekrutacyjne  5      &     ZadanieRekrutacyjne.EmployeeList   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:ZadanieRekrutacyjne     5        � 	Resources  5	          BooleanToVisibilityConverter��%   &	        �� 5
   
   6'   5      .��5      �� .+�5   
   � .+�5      &   System.Windows.Controls.DataGrid     AutoGenerateColumns$ False��5         GridLinesVisibility$
 None=�5         CanUserAddRows$ false��5      $	��350��5      "   VerticalScrollBarVisibility$
 Auto=�5      ��5      �� *EmployeesList+   Columns 5      4  .System.Windows.Controls.DataGridCheckBoxColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$  ��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ 30 5   -   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5   -   �� *
IsSelected+5   -   0  *System.Windows.Controls.DataGridTextColumn  5      $ Numer pracownika��6)   $	 120 5   )   	   
IsReadOnly$
	 True��5   )    5   )   �� *EmployeeNumber+5   )     5      $ Imię��6)   $	 100 5   )   $
	 True��5   )    5   )   �� *Name+5   )     5       $ Nazwisko��6)   $	 150 5"   )   $
	 True��5#   )    5!   )   �� *
LastName+5#   )     5$      $ 
Stanowisko��6)   $	 150 5&   )   $
	 True��5'   )    5%   )   �� *
Position+5'   )     5(      $ Dział��6)   $	 150 5*   )   $
	 True��5+   )    5)   )   �� *
Department+5+   )   5-      5.      5/      50      