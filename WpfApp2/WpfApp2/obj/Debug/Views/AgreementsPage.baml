   M S B A M L   `   `   `  ����B  >WpfApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfApp2.ViewsWpfApp2.Views  5      "    WpfApp2.Views.AgreementsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:WpfApp2.Views     5        N� Title$  Договоры��5	      .��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��0,0,0,10q�6;   .+�5      �� $7��1Поиск по клиенту (ИНН/Шифр):��6   �� 5      -   5         ��Name$ SearchClientTextBox��5   3   $	��200��6   $��5,0,5,0q�6"   63   �� 5      -   5        $��
Найти��5      $��80��6%   60   �� 5      -   5        $��Показать все��5      $	��100��6,   $��5,0,0,0q�68   6I   5      �� 5   
   $��1��6   $ 
Horizontal=�6"   $��0,0,0,10q�6;   .+�5      �� -   5        $��Добавить��5      $	��100��6(   $��0,0,5,0q�64   6E   �� 5      -   5        $ ��Редактировать��5      $	��100��6-   $��0,0,5,0q�69   6J   �� 5      -   5        $��Удалить��5      $	��100��6'   $��0,0,5,0q�63   6D   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ AgreementsGrid��5       $��2��6      AutoGenerateColumns .     
IsReadOnly .    SelectionMode$ Single=�6f      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 
Номер��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6O   1  +System.Windows.Controls.DataGridBoundColumn   Binding 64   �� *Number+6O     5      $	 Клиент (ИНН)��6%   $	
 150 6[    6;   �� *Persons.Inn+6[     5!      $	 Тип договора��6%   $	
 150 6Z    6;   �� *
Types.Name+6Z     5"      $#	 Статус договора��6%   $	
 150 6j    6>   �� *StatusAgreements.Status+6j     5#      $	 Дата открытия��6%   $	
 120 6t    6<   �� *
DataOpen+ �� StringFormat$ 
dd.MM.yyyy��6t   5%      5&      5'      