   M S B A M L   `   `   `  ����B  >WpfApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfApp2.ViewsWpfApp2.Views  5          WpfApp2.Views.PersonsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:WpfApp2.Views     5        N� Title$  Клиенты��5
      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��0,0,0,10q�6;   .+�5      �� -   5        $��Добавить��5      $	��100��6(   $��0,0,5,0q�64   6E   �� 5      -   5        $ ��Редактировать��5      $	��100��6-   $��0,0,5,0q�69   6J   �� 5      -   5        $��Удалить��5      $	��100��6'   $��0,0,5,0q�63   6D   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ PersonsGrid��5       $��1��6      AutoGenerateColumns .     
IsReadOnly .    SelectionMode$ Single=�6c      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ИНН��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6J   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 62   �� *Inn+6J     5      $ Тип клиента��6%   $		 150 6S   
 6:   �� *Type+6S     5      $ Шифр��6%   $		 120 6N   
 63   �� *Shifer+6N     5      $% Дата регистрации��6%   $		 150 6s   
 6?   �� *Data+ �� StringFormat$ 
dd.MM.yyyy��6s   5      5      5      