   M S B A M L   `   `   `  ����F  BProductDEmo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ProductDEmoProductDEmo  5   	       ProductDEmo.AdminWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:ProductDEmo     5   	     � Title$  AdminWindow��5   	   $	��550��6   $	��800��6*    � WindowStartupLocation$ CenterScreen=�66    � Icon$ Resources/icon.ico��6[   .��5	      �  � RowDefinitions 5
   
   �� 5      $��75 �6   6*   �� 5      6   �� 5      $��75 �6   6*   .+�5   
   �� $��0��6   ���#468C5165   �� 5   
   $]��WЗдесь вы можете добавлять/редактировать товары��6   $��Center=�6J   $��Center=�6g      StandartTextBlock#��[  6�   6�   �� 5   
   $��2��6   ���#468C5165   �� 5   
   -   5        
������$��
Назад��5   )   $	��150��69   $
��Left=�6E     StandartButton#��[ 6`   $��4��6�   $��10q�6�    � 	Resources 5       �� 
TargetType �� Property �� Value��%d   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$ 20��65   6B   5      5      5      �� 5   
   -   5        
������$!��Добавить товар��5   )   $	��195��6B   $��Center=�6N   #��[ 6k   $��4��6�   $��10q�6�    5      ��%d   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$ 20��65   6B   5      5      5      &   System.Windows.Controls.DataGrid  5    
   -   5        $��AdminProductGrid��5    4   $��16(�6L   $��1��6Z   $��5q�6g      AutoGenerateColumns .  	   Columns	 5!      0  *System.Windows.Controls.DataGridTextColumn  5"      ,  &System.Windows.Controls.DataGridColumn
   Header$
 ID��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *	ProductID+6Q     5#      $
 Article��6%    66   �� *ProductArticleNumber+6a     5$      $

 Name��6%    63   �� *ProductName+6U     5%      $
 UnitType��6%    67   �� *UnitType.UnitTypeName+6c     5&      $

 Cost��6%    63   �� *ProductCost+6U     5'      $
 MaxDiscount��6%    6:   �� *ProductMaxDiscountAmount+6i     5(      $
 Manufacturer��6%    6;   �� *-+ProductManufacturer.ProductManufacturerName+6}     5)      $
 Supplier��6%    67   �� *%#ProductSupplier.ProductSupplierName+6q     5*      $
 Category��6%    67   �� *%#ProductCategory.ProductCategoryName+6q     5+      $
 Discount��6%    67   �� *ProductDiscountAmount+6c     5,      $
 QuantityInStock��6%    6>   �� *ProductQuantityInStock+6k     5-      $
 Description��6%    6:   �� *ProductDescription+6c   4  .System.Windows.Controls.DataGridTemplateColumn  5.      $
 Photo��6)      CellTemplate 5/      �� 50      .R�51      �� $��50��6$   ��6/   �� *
FullPath+ �� TargetNullValue$ Resources/picture.png��6t   52      54      56      57      58      