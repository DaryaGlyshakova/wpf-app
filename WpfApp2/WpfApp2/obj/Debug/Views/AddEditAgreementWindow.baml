   M S B A M L   `   `   `  ����B  >WpfApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfApp2.ViewsWpfApp2.Views  5   	   *    $WpfApp2.Views.AddEditAgreementWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:WpfApp2.Views     5   	     � Title$H  BДобавление/Редактирование договора��5   	   $	��450��64   $	��450��6A    � WindowStartupLocation$ CenterOwner=�5	   	    � 
ResizeMode$ NoResize=�6-    � ShowInTaskbar .  .��5
      � $��15q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5"   
   �� $��0��6   $��0��6   $"��Номер договора:��6-   $��Center=�6G   $��0,0,5,5q�6b   �� 5#   
   -   5         ��Name$ NumberTextBox��5#   /   $��0��6   $��1��6   $��0,0,0,5q�6F   $��Center=�6W   �� 5%   
   $��1��6   $��0��6   $��Клиент:��6-   $��Center=�6?   $��0,0,5,5q�6Z   �� 5&   
   -   5         ��Name$ PersonComboBox��5&   0   $��1��6   $��1��6    $��0,0,0,5q�6H   $��Center=�6Y    �� DisplayMemberPath$	 Inn��5'      	 �� SelectedValuePath$	 ID��6+   �� 5*   
   $��2��6   $��0��6   $��Тип договора:��6-   $��Center=�6E   $��0,0,5,5q�6`   �� 5+   
   -   5        $ TypeComboBox��5+   0   $��2��6   $��1��6    $��0,0,0,5q�6F   $��Center=�6W   $
 Name��5,      $	 ID��6,   �� 5.   
   $��3��6   $��0��6   $$��Статус договора:��6-   $��Center=�6H   $��0,0,5,5q�6c   �� 5/   
   -   5        $ StatusAgreementComboBox��5/   0   $��3��6   $��1��6    $��0,0,0,5q�6Q   $��Center=�6b   $ Status��50      $	 ID��6.   �� 52   
   $��4��6   $��0��6   $ ��Дата открытия:��6-   $��Center=�6F   $��0,0,5,5q�6a   (  "System.Windows.Controls.DatePicker  53   
   -   5        
  Name$
 DataOpenDatePicker��53   2   $��4��6   $��1��6"   $��0,0,0,5q�6N   $��Center=�6_   �� 55   
   $��5��6   $��0��6   $ ��Дата закрытия:��6-   $��Center=�6F   $��0,0,5,5q�6a     56   
   -   5        $
 DataCloseDatePicker��56   2   $��5��6   $��1��6"   $��0,0,0,5q�6O   $��Center=�6`   �� 58   
   $��6��6   $��0��6   $��Примечание:��6-   $	��Top=�6C   $��0,5,5,5q�6[   �� 59   
   -   5        $ NoteTextBox��59   /   $��6��6   $��1��6   $��0,5,0,5q�6D    �� TextWrapping$
 Wrap=�6U    �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�6~   $��60��6�   �� 5;   
   $��7��6   $��0��6"   $��2��62    �� Orientation$ 
Horizontal=�6F   $��Right=�6_   $��0,10,0,0q�6{   .+�5<      �� -   5        $��Сохранить��5<      $	��100��6)   $��0,0,5,0q�65    �� 	IsDefault . 6_   �� 5=      -	   5        $��Отмена��5=      $	��100��6&    �� IsCancel . 6M   5>      5?      5@      