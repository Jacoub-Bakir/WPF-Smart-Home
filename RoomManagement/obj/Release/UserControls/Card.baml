   M S B A M L   `   `   `  ����   RoomManagement&     RoomManagement.UserControls.Card   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -   $
��card��.���� $	��150��  �� Padding$  20q�$��Stretch=� �� CornerRadius$ 25�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 20Q�   Opacity$	 0.5Q�   ShadowDepth$ 1Q�����  �� 
TargetType" ��.���  �� Property � �� �� Value$	 150��	 �� Triggers	 �� 
 �� Binding
 ��  �� ElementName$ toggle�� �� Path$ 	IsChecked� �� Value True.a���  � �� �#3256F1�� 
 �� $ toggle��$ 	IsChecked�	 False.a���  � �� �#EFEFEE�� 
 �� $
 card��$ IsHorizontal� True.a���  � ��$	 190���� 
 �� $
 card��$ IsHorizontal�	 False.a���  � ��$	 150��._�� .+��� $��0,5,0,0q�$
��Left=�$	��Top=�$��SemiBold"����� " ��	 �� 
 �� $ toggle��$ 	IsChecked� True.a���  � ��$ On����  � �� �#ffffff�� 
 �� $ toggle��$ 	IsChecked�	 False.a���  � ��$	 Off����  � �� �#3256F1�� -    ��Name$ toggle��$��Right=� d� 	IsChecked �� $
 card��$ 	IsChecked�����    toggleButton �� BasedOn# [  " ��.���  � ��$	 Top=�	 �� 
 �� $
 card��$ IsHorizontal� True.a���  � ��$ Center=��� 
 �� $
 card��$ IsHorizontal�	 False.a���  � ��$	 Top=��� $��40��$��40��$
��Left=�$��Center=����� " ��	 �� 
 �� $ toggle��$ 	IsChecked� True.a���  � �� �� $
 card��$ ImageOn��� 
 �� $ toggle��$ 	IsChecked�	 False.a���  � �� �� $
 card��$ ImageOff��� $��Bottom=�$��SemiBold"����� $
 card��$ Title����� " ��	 �� 
 �� $ toggle��$ 	IsChecked� True.a���  � �� �#ffffff�� 
 �� $ toggle��$ 	IsChecked�	 False.a���  � �� �#2F2C37