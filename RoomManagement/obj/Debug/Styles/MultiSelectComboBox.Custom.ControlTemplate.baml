   M S B A M L   `   `   `  ����\  XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZSdl.MultiSelectComboBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a6f9048fb51b33ed  �� MergedDictionaries �� Source #  MultiSelectComboBox.FocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  MultiSelectComboBox.Style �� 
TargetType . *MultiSelectComboBox.DefaultTextBlock.Style ' #MultiSelectComboBox.Text.FontFamily ' #MultiSelectComboBox.Text.Foreground �� Triggers �� Binding	 �� RelativeSource
 �� AncestorType �� Value 0 ,MultiSelectComboBox.Text.Disabled.Foreground D @MultiSelectComboBox.SelectedItemsPanel.WrapableItemsSource.Style � Orientation - )MultiSelectComboBox.DropDown.Button.Style 2 .MultiSelectComboBox.DropDown.Button.Background .	 *MultiSelectComboBox.DropDown.Button.Border �� 
TargetType ��Name ��Name �� Triggers P� Property P� Value �� 
TargetName <
 8MultiSelectComboBox.DropDown.Button.MouseOver.Background 8 4MultiSelectComboBox.DropDown.Button.MouseOver.Border : 6MultiSelectComboBox.DropDown.Button.Pressed.Background 6 2MultiSelectComboBox.DropDown.Button.Pressed.Border ; 7MultiSelectComboBox.DropDown.Button.Disabled.Background 7 3MultiSelectComboBox.DropDown.Button.Disabled.Border ; 7MultiSelectComboBox.DropDown.Button.Disabled.Foreground ; 7MultiSelectComboBox.Dropdown.ListBox.ItemContainerStyle:   4Sdl.MultiSelectComboBox.Controls.ExtendedListBoxItem �� Path �� Padding    	IsChecked d� 	IsChecked �� CornerRadius @ <MultiSelectComboBox.DropDown.ListBoxItem.Selected.Background@  :Sdl.MultiSelectComboBox.Themes.Generic.MultiSelectComboBox < 8MultiSelectComboBox.DropDown.ListBoxItem.Selected.Border @ <MultiSelectComboBox.DropDown.ListBoxItem.Selector.Background . *MultiSelectComboBox.Dropdown.ListBox.Style6  0Sdl.MultiSelectComboBox.Controls.ExtendedListBox �� Self B >MultiSelectComboBox.SelectedItemsPanel.RemoveItem.Button.Style < 8MultiSelectComboBox.SelectedItem.Button.Light.Foreground < 8MultiSelectComboBox.SelectedItem.Button.Hover.Background 9 5MultiSelectComboBox.SelectedItems.ItemContainer.Style + 'MultiSelectComboBox.SelectedItem.Border �� Orientation ��Name � ToolTip �� Stretch ^� 
Conditions �� Binding  �� Value B >MultiSelectComboBox.SelectedItemsPanel.EditMode.Glyph.Template 6 2MultiSelectComboBox.SelectedItem.Button.Foreground B >MultiSelectComboBox.SelectedItemsPanel.Readonly.Glyph.Template 3 /MultiSelectComboBox.SelectedItemsPanel.Template! � ColumnDefinitions:  4Sdl.MultiSelectComboBox.Controls.ContentItemsControl"  Name# � 	Resources  Key$ ��Name% � ForceCursor& �� TextWrapping' �� 
IsReadOnly 8  4MultiSelectComboBox.Dropdown.ListBox.Header.Template( �� DataType) �� Padding ?! ;MultiSelectComboBox.DropDown.ListBox.GroupHeader.Foreground ?" ;MultiSelectComboBox.DropDown.ListBox.GroupHeader.Background $#  MultiSelectComboBox.Custom.Style* �Name-   'System.Windows.Input.KeyboardNavigation+   TabNavigation, �� Mode- �Name. � ActualWidth/ � AllowsTransparency0 �� ElementName1 � PlacementTarget2 �� ClearTypeHint3 ��Name4 ��Name5  Name6 �� 
GroupStyle(   "System.Windows.Controls.GroupStyle7   HeaderTemplate�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %sdlhttp://schemas.sdl.com/xaml  5        5      �� 5   
   $( "MultiSelectComboBox.Resources.xaml?�6   %�0  &	        &	 �     &	 #    7  7  7  &	 �    7  &	 �    7  7 	 7 
 7  7  7  7  7  7  &	 �    7   7  7  7  7  7  7  7  &	 �    7  7  &	 %    7  7  &	 �    7  7  7  &	 0     7  &	 �     7  &	 �!    7  7  7  7  7  &	  �'    7  7 ! 7 " &	# m(    7  7  7  7   7  �� 5	      .�5
   
   ��  � �� 5      �� 5      .R�5      �� $��2q�6     . #��� ��6F   $��1��6�   $	 1 2R�6�   5      5      5      �� 5      " �6.   .�5   
   ��  � ��$
 Auto��6#   �� 5   
    � ��$
 Auto��6"   �� 5   
    �@J�SnapsToDevicePixels . 60   �� 5   
    � ��$ 0q�6#   �� 5   
    �@�OverridesDefaultStyle . 62   �� 5   
   " �@%�IsVirtualizingWhenGrouping . 6I   �� 5   
     �@%�IsContainerVirtualizable . 6G   5      �� 5      " ��6?   .�5   
   ��  � ��8   6'   �� 5   
    � ��$ 11(�6%   �� 5    
    � ��$ Stretch=�60   �� 5!   
    � ��$ Center=�6.   �� 5"   
    � ��$ Center=�6=   �� 5#   
    � ��$ NoWrap=�6)   �� 5$   
    � ��$ CharacterEllipsis=�6)   �� 5%   
    �@��Padding$ 1q�6$   �� 5&   
    � ��$ 1q�6#   �� 5'   
    � ��8  6'   �� 5(   
    �@��TextAlignment$
 Left=�6*    5)   
   �� 5*       6   �� *	IsEnabled+	 �� "
 d�	 False6|   .a�5+      ��  � ��8  6/   5,      5.      �� 50      " ��6U   .�51   
   ��  � ��8   6'   �� 52   
    � ��$ 11(�6%   �� 53   
    �@J�SnapsToDevicePixels . 60   �� 54   
    �@�OverridesDefaultStyle .  62   �� 55   
    � ��
 ���� 6'   �� 56   
    � �� .  6&   �� 57   
    � �� 58      �� 59      .R�5:      �� ��. $
��Auto=�6;   $��Disabled=�6^   .��5;      �� 5<      5=      5?      �� 5@   
    � �� 5A      �� 5B      .R�5C      � $ 
Horizontal=�6    ��69   �� *SelectedItemsPanelBackground+	 �� $
 sdl:MultiSelectComboBoxL�5D      5F      5G      �� 5I      " ��6>   .�5J   
   ��  � ��8   6'   �� 5K   
    � ��8  6(   �� 5L   
    � ��# � ��6'   �� 5M   
    � ��$ 0q�6,   �� 5N   
    � ��$ Center=�67   �� 5O   
    � ��$ Center=�65   �� 5P   
    � ��$ 0,0,2,0q�6$   �� 5Q   
    � �� .  6&   �� 5R   
    � �� 5S      �� 5T      " ��6"   .R�5U      �� $ border��6   #��z��6-   #��z��6Y   #��z��5V   +    . ._�5W      �� $ contentPresenter��6+   #��z��6E   #��z��5X   +   ��.  # z 5Y   +   #��z��5Z   +   5[       5\      P� 5]       �@��IsDefaulted . . �5^      �� $ border��6<   # � ��6P    � ��5_      P� 5`       �@J�IsMouseOver . . �5a      �� $ border��6;   8   � ��6O   �� 5b      $ border��6<   8   � ��6P   5c      P� 5d       � �� . . �5e      �� $ border��6;   8   � ��6O   �� 5f      $ border��6<   8   � ��6P   5g      P� 5h       � {� .  . �5i      �� $ border��6;   8   � ��6O   �� 5j      $ border��6<   8   � ��6P   �� 5k      $ contentPresenter��6G   8   � ��6e   5l      5n      5p      5q      �� 5s      "   6L   .�5t   
   ��  �@J�SnapsToDevicePixels . 60   �� 5u   
    � ��$ 1q�6$   �� 5v   
    � ��$ 0q�6#   �� 5w   
    � �� 67   �� *HorizontalContentAlignment+	 �� "
 ���� 5x   
    � �� 65   �� *VerticalContentAlignment+	 �� "
 ���� 5y   
    � ��
 ���� 6'   �� 5z   
    � ��
 ���� 6(   �� 5{   
    � ��$ 0q�6,   �� 5|   
    � {� 6&   �� $ 	IsEnabled��� 5}   
    �@�FocusVisualStyle8   6-   �� 5~   
    � �� 5      �� 5�      "   6"   .R�5�      �� $ Bd��6   #��z��6)   #��z��6U   #��z��6�   # z��6�    . ._�5�      d� # z 6'   
�������$��Stretch=�6c   $��0q�6�   $��0q�6�   $��0q�6�   ��5�      �� 5�   "   " d�62   .R�5�   &   �� $ border��6-   $��1q�6=   $ 5��6Q   $��Stretch=�6b   8�� ._�5�   *   �� $��	2,-2,0,-2q�69   ��6L   �� ��6`   �� 	 �� *FindAncestor+"
  $" DropdownItemTemplateSelector�5�   '    5�   &   P� 5�   *    �@��IsDefaulted . . �5�   .   �� $ border��6L   8   � ��6`   5�   +   P� 5�   *    �@J�IsMouseOver . . �5�   .   �� $ border��6K   8   � ��6_   �� 5�   .   $ border��6L   8   � ��6`   5�   +   P� 5�   *    �@d�	IsChecked$ falseY�6G   . �5�   .   �� $ border��6L    � ��
 ���� 6`   5�   +   P� 5�   *    �@d�	IsChecked$
 trueY�6G   . �5�   .   �� $ border��6K   8   � ��6_   �� 5�   .   $ border��6L   8   � ��6`   5�   +   5�   #   5�      5�       5�      P� 5�       � {� .  . �5�      �� $ Bd��6G   # � ��6W    � ��5�      P� 5�       �@��
IsSelected . . �5�      �� $ Bd��6;   8   � ��6K   5�      5�      5�      5�      �� 5�      "  6?   .�5�   
   ��  � ��8   6'   �� 5�   
    � ��$ 11(�6%   �� 5�   
    �@w�TextPath$
 Name��60   �� 5�   
    �@��IsTextSearchEnabled . 60   �� 5�   
   % �@��IsSynchronizedWithCurrentItem$
 TrueY�6:   �� 5�   
   ! �@��IsTextSearchCaseSensitive .  66   �� 5�   
    �@��SelectionMode$ Single=�6*   �� 5�   
    � ��$ Stretch=�67   �� 5�   
   " �@%�IsVirtualizingWhenGrouping . 6I   �� 5�   
     �@%�IsContainerVirtualizable . 6G   �� 5�   
    �@%�IsVirtualizing . 6B   �� 5�   
    � ��$ Center=�65   �� 5�   
    � ��8  6/   �� 5�   
    � �� 5�      �� 5�      .R�5�      $� ��6-   �� *DropDownPopupBackground+	 �� $
 sdl:MultiSelectComboBoxL�5�      5�       5�   
   �� 5�       6   �� #	 Z $ Items.Count� 06n   .a�5�      ��  � �� 5�      �� 5�      .R�5�      �� $ 4q�6%   $��1q�61   
�������._�5�   "   �� No results found.6?   5�      5�      5�      5�      5�      �� 5�      " ��6S   .�5�   
   ��  � ��8   6'   �� 5�   
    � �� 6'   Z� �� 5�   
    � �� 6(   Z� �� 5�   
    � ��$ 0q�6,   �� 5�   
    � ��$ 0q�6$   �� 5�   
    � ��$ 	-1,0,0,-1q�6#   �� 5�   
    �@�Cursor$
 Hand��6#   �� 5�   
    � �� .  6&   �� 5�   
    � ��$ Center=�67   �� 5�   
    � ��$ Center=�65   �� 5�   
    � ��$ Center=�60   �� 5�   
    � ��$ Center=�6.   �� 5�   
    � ��$ 16��6#   �� 5�   
    � ��$ 16��6"   �� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      �� $ border��6   #��z��6-   $��1,0,1,1q�6a   
������ 
������ ._�5�      �� $��0q�6+   $��Center=�66   $��Center=�6S   ��.  6n   5�       5�      P� 5�       �@J�IsMouseOver . . �5�      �� $ border��6;   8   � ��6O   �� 5�      $ border��6<    6P   Z�  � ��5�      �� 5�      $ False��6�    6&   �� 	 �� *FindAncestor+"
  $ 
IsEditMode�.a�5�      ��  � y�$ 	Collapsed=�6;   5�      �� 5�      $ False��6�    6&   �� 	 �� *FindAncestor+"
  $ 
IsEditable�.a�5�      ��  � y�$ 	Collapsed=�6;   5�      �� 5�       6&   ��  6:   Z� .a�5�      ��  � y�$ 	Collapsed=�6;   5�      5�      5�      5�      �� 5�      " ��6J   .�5�   
   ��  � �� 5�      �� 5�      " ��6"   .R�5�      �� $ Bd��6   $��1q�6)   $ 5��6=   $��1q�6N   8��  ._�5�      �� $ 
Horizontal=�6%   .+�5�      �� $��2,2,2,4q�6A   ��6-   �� ��6R   �� 	 �� *FindAncestor+"
  $" SelectedItemTemplateSelector��� 5      $C =PART_MultiSelectComboBox_SelectedItemsPanel_RemoveItem_Button��6%   $ Remove��6l   8�� .��5  "   F�  .  $��3q�6C   $��Stretch=�6N   $��Stretch=�6l   $ Uniform=�6�   8�� |��������<R��C�Z��@��=R��m5�����O���C��m5��Ѥ���n5��C0P������n5�H<R�����Z���CѬ<R\�������<R5  '   5     5     5      5     ^� 5      5	     �� 5
  "    6,   ��   6@   Z� 0�5     �� 5  "   $ Bd��6@    6P   Z�  � ���� 5  "   $ Bd��6;    � ��$ 1,0,1,0q�6K   5     5     5     5     �� 5     .R�5  
   F� $��Center=�6   $��Center=�6,   "����        � 	=  	= � z     	8��  5     5     �� 5     .R�5  
   F� $��Center=�6   $��Center=�6,   $��13��6G   �����p< lk ���A�xe ��A��[ p<0�U ��M�32 Ӏ� @, p� @,  ��32 р��   N ���pX� ���- �6���- @o@�0�f @o@�P��� ���� �L^ ���- �6	8��  $ Uniform=�5  Z   5      �� 5"     .R�5#  
   �� ���	#FFE1E6EB$��1q�6)   
�������$ 5��6P   ._�5$     � ! 5%     �� 5&     $��* �6'   �� 5'     $��20 �6'   .+�5*     �� $��0��6   $��1q�6)   ._�5+       $>" 8PART_MultiSelectComboBox_SelectedItemsPanel_ItemsControl��6.   8��  8�� # 5.     ���� 5/     2.MultiSelectComboBox.SelectedItems.ItemTemplate .R�50  "   �� #���  6>   $	��2,0q�6�   ��6,   �� 6�   51     �� 52     =9MultiSelectComboBox.SelectedItems.Searchable.ItemTemplate .R�53  "   �� $ 
Horizontal=�6-   .+�54  &   �� $@$ :PART_MultiSelectComboBox_SelectedItemsPanel_Filter_TextBox��6.   |�. ��.  $��0q�55  M   % . $��0q�6k   
������ 
������ $��1q�6�   $& NoWrap=�6�   {�54  r   �� *
IsEditMode+	 �� $
 sdl:MultiSelectComboBoxL�55  �   �� 56  &   $M$ GPART_MultiSelectComboBox_SelectedItemsPanel_Filter_AutoComplete_TextBox��6.   ' . |�.  ��.  $��-2,0,0,0q�57  N   $��0q�6`   
������ 
������ $��-3,1,0,1q�6�   $& NoWrap=�6�   58  #   59     5:     5;     5<     �� 5>     $. (PART_MultiSelectComboBox_Dropdown_Button��6   $��1��6K   8�� .��5?     �� ��5@     �� 5A     " ��6$   .�5B  "   ��  � �� .  6>   �� 5C  "    � ��8  6D    5D  "   �� 5E  &    62   �� 	 �� *FindAncestor+"
  $ 
IsEditMode� True6�   .a�5F  *   ��  � ��8  6L   5G  '   5I     5K     5M     5N     5O     5P     �� 5R     $( sdl:IItemGroup��6P   .R�5S  
   �� $��CharacterEllipsis=�6/   $
) 7, 5q�6Q   $��0,0,0,2q�6`   $
��Bold"�6q   8��  8�� 8�� ��6   �� $
 Name�6  5T     �� 5V     "  65   .�5W  
   ��  � �� 5X     �� 5Y     "  6"   .R�5\     � $* PART_MultiSelectComboBox��6   8��  .+�5]     d� $
+ None=�6'   ��.   6O   �� $ IsDropDownOpen�$, TwoWay=�	 �� *TemplatedParent+��5^     �� 5_  "   " d�62   .R�5`  &   �� 8�� 65   5a  #   5c     � 5e     $'- !PART_MultiSelectComboBox_Dropdown��6    $��2q�6�   $��2��6�   #��z. 6�  $��Bottom=�6�  / . $��Slide=�6  ��6K   �� *ActualWidth+$0 PART_MultiSelectComboBox����6�   �� $ MaxDropDownHeight�	 �� *TemplatedParent+1 6  �� $0 PART_MultiSelectComboBox����6Q  �� $ IsDropDownOpen�$, TwoWay=�	 �� *TemplatedParent+.��5f     �� $ dropDownBorder��6%   $��0,1,0,0q�6=   #��� ��6N   $��0q�6�   #��� ��6�   ._�5g  &   � $
* grid��6+   $2 Enabled=�69   .+�5h  *   �� $3 canvas��61   $
��Left=�6A   $��0��6\   $	��Top=�6g   $��0��6   .+�5i  .   �� $4 
opaqueRect��68   ��6L   �� *
Background+$0 dropDownBorder����6�   �� *ActualHeight+$0 dropDownBorder����6�   �� *ActualWidth+$0 dropDownBorder��5j  +     5k  *   $/5 )PART_MultiSelectComboBox_Dropdown_ListBox��6>   8�� 6 5l  .     5m  2   87  6=   # 5o  .   ���� 5p  2   51MultiSelectComboBox.Dropdown.ListBox.ItemTemplate .R�5q  6   �� 8�� $��2,2,2,3q�6�   ��6@   �� 6�   5r  3   5s  /   5t  +   5u  '   5v  #   5w     5x      5z     �� 5{     $ Stretch��6�    6&   �� *VerticalAlignment+	 �� $
 sdl:MultiSelectComboBoxL�.a�5|     �� $ PART_MultiSelectComboBox��6%    6`   �� *ActualHeight+	 �� $
 sdl:MultiSelectComboBoxL� � ��5}     5�     5�     5�     5�     