   M S B A M L   `   `   `  ����  RoomManagement&     RoomManagement.UserControls.Card   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5        $
��card��5      .��5
      �� $	��150��5   	     �� Padding$  20q�5   	   $��Stretch=�5   	    �� CornerRadius$ 25��5   	    J� Effect 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 20Q�5         Opacity$	 0.5Q�5         ShadowDepth$ 1Q�5      ��5   
   �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value$	 150��6*   	 �� Triggers	 5      �� 5      
 �� Binding
 6"   ��  �� ElementName$ toggle�� �� Path$ 	IsChecked� �� Value True6Y   .a�5      ��  � �� �#3256F167   5      �� 5      
 6"   �� $ toggle��$ 	IsChecked�	 False6Y   .a�5      ��  � �� �#EFEFEE67   5       �� 5"      
 6"   �� $
 card��$ IsHorizontal� True6Z   .a�5#      ��  � ��$	 190��62   5$      �� 5&      
 6"   �� $
 card��$ IsHorizontal�	 False6Z   .a�5'      ��  � ��$	 150��62   5(      5*      ._�5-   
   � .+�5.      �� $��0,5,0,0q�5/      $
��Left=�50      $	��Top=�51      $��SemiBold"�52      ��53      �� 54      " ��6   	 55      �� 56      
 6*   �� $ toggle��$ 	IsChecked� True6a   .a�57   "   ��  � ��$ On��69   �� 58   "    � �� �#ffffff6?   59      �� 5;      
 6*   �� $ toggle��$ 	IsChecked�	 False6a   .a�5<   "   ��  � ��$	 Off��69   �� 5=   "    � �� �#3256F16?   5>      5@      5B      �� 5C      -   5         ��Name$ toggle��5D      $��Right=�5H      
 J� Uid 5E      �� $
 card��$ Card_ID� d� 	IsChecked 5I      �� $
 card��$ 	IsChecked���5J      �� 5K         toggleButton �� BasedOn# [  6   " ��6D   .�5L      ��  � ��$	 Top=�6>   	 5M      �� 5N      
 6*   �� $
 card��$ IsHorizontal� True6b   .a�5O   "   ��  � ��$ Center=�6F   5P      �� 5R      
 6*   �� $
 card��$ IsHorizontal�	 False6b   .a�5S   "   ��  � ��$	 Top=�6F   5T      5V      5X      �� 5Z      $��40��5[      $��40��5\      $
��Left=�5]      $��Center=�5^      ��5_      �� 5`      " ��6   	 5a      �� 5b      
 6*   �� $ toggle��$ 	IsChecked� True6a   .a�5c   "   ��  � �� 6;   �� $
 card��$ ImageOn�5d      �� 5f      
 6*   �� $ toggle��$ 	IsChecked�	 False6a   .a�5g   "   ��  � �� 6;   �� $
 card��$ ImageOff�5h      5j      5l      �� 5n      $
��Wrap=�5o      $��Bottom=�5p      $��SemiBold"�5q      ��5r      �� $
 card��$ Title���5s      �� 5t      " ��6   	 5u      �� 5v      
 6*   �� $ toggle��$ 	IsChecked� True6a   .a�5w   "   ��  � �� �#ffffff6?   5x      �� 5z      
 6*   �� $ toggle��$ 	IsChecked�	 False6a   .a�5{   "   ��  � �� �#2F2C376?   5|      5~      5�      5�      5�      5�      