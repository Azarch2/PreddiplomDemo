   M S B A M L   `   `   `  ����F  BProductDEmo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ProductDEmoProductDEmo  5   	       ProductDEmo.CaptchaWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:ProductDEmo     5   	     � Title$  CaptchaWindow��5   	   $	��240��6   $	��400��6,    � WindowStartupLocation$ CenterScreen=�68   .��5	      �  � RowDefinitions 5
   
   �� 5      $��30 �6   6*   �� 5      6   .+�5   
   �� ���#468C516(   �� 5   
   $��18(�6   $P��JВы должны пройти капчу, чтобы продолжить��6"   $��Center=�6R   $��Center=�6o      StandartTextBlock#��[  6�   6�   �� 5   
   
���   �$
��Bold"�6'   $��1��69   $��18(�6F   $)��#Введите числа ниже:��6T   $��20q�6o   $��Center=�6{   $	��Top=�6�   #��[  6�   6�   �� 5   
   -   5        $��CaptchaTextBlock��5      
���   �$��1��6@   $��18(�6M   $
��5555��6[   $��40q�6g   $��Center=�6s   $	��Top=�6�   #��[  6�   6�   �� 5   
   -   5        $��TextBoxNumbers��5      $��1��6(     StandartTextBox#��[ 65   $	��200��6^   $��0,25,0,0q�6j   $��35��6}   6�   �� 5   
   -   5        $��1��5   +   $	��175��68   $��40��6D     StandartButton#��[ 6P   $��Bottom=�6x   $��3q�6�   $��Ввести��6�   
����������#76E383 � 	Resources 5       �� 
TargetType �� Property �� Value��%d   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$ 20��65   6B   5      5      5      5      5      