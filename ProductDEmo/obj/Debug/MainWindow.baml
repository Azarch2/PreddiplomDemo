   M S B A M L   `   `   `  ����F  BProductDEmo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ProductDEmoProductDEmo  5   	       ProductDEmo.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:ProductDEmo     5   	     � Title$  Авторизация��5   	   $	��450��6   $	��800��6*    � WindowStartupLocation$ CenterScreen=�66    � Icon$ Resources/Icon.ico��6[   .��5	      �  � RowDefinitions 5
   
   �� 5      $��75 �6   6*   �� 5      6   �� 5      $��75 �6   6*   .+�5   
   �� $��0��6   ���#468C5165   �� 5   
   $��0��6   $��5q�6   $
��Left=�6(   $��60��6C   $��Resources/logo.png��6N   6l   �� 5   
   $`��ZДобро пожаловать, здесь вы можете авторизоваться��6   $��Center=�6M   $��Center=�6j      StandartTextBlock#��[  6�   6�   �� 5   
   $��2��6   ���#468C5165   �� 5   
   $��1��6   $��Center=�6"   $��Center=�6?   .+�5      ��   AnotherTextBlock#��[ 6   $��Введите логин��6B   6Y   �� 5      -   5        $��LoginTextBox��5      $	��200��6*     StandartTextBox#��[ 66   6b   �� 5      #��[ 6   $!��Введите пароль��6B   6Z   �� 5      -   5        $��PasswordTextBox��5      $	��200��6-   #��[ 69   6e   5      �� 5   
   -   5        $"��Авторизоваться��5      $	��200��6*     StandartButton#��[ 66   $��2��6^   $��10q�6k    � 	Resources 5       �� 
TargetType �� Property �� Value��%d   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$ 20��65   6B   5      5      5       �� 5!   
   -   5        
���  ��$��
Выйти��5!   "   $	��150��62   $
��Left=�6>   #��[ 6Y   $��2��6�   $��10q�6�    5"      ��%d   '��       �� 5#      " ��6   .�5$      ��  �@��CornerRadius$ 20��65   6B   5%      5&      5'      �� 5(   
   -   5        $��Товары��5(      $	��150��6#   $��Right=�6/   #��[ 6K   $��2��6s   $��10q�6�    5)      ��%d   '��       �� 5*      " ��6   .�5+      ��  �@��CornerRadius$ 20��65   6B   5,      5-      5.      5/      50      