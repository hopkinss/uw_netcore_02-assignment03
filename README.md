# uw_netcore_02-assignment03:  Sortable Lists

## Required Functionality
ViewModels\ViewModel.cs OnSort method sorts the list by clicking the desired gridview header. Converters\ListHeaderImageConverter.cs converts the sort direction
properties (IsNameSortAscending and IsPasswordSortAscendintg) in the view model into the appropriate direction icons.

## Additional functionality 
<ol>
<li>I extracted the User into a simpler class in Models and refactored the INotifyProperty and NotifyDataErrorInfo into base classes inherited by the ViewModel. </li>
<li>changed startup in app.xaml to inject the data into the view model  </li>
<li>I moved the Listview and the selected item details into separate user controls in the Views folder </li>
<li>Added CRUD on User operations collection </li>
<li>Added validation and password strength indicator on the Name and Password fields  </li>
</ol>

## Questions
I would have rather used separate viewmodels for the list and detail view as the viewmodel was already getting too big. What's the best way to share data between view models? I 
started reading about Prism's event aggregator that follows a pub/sub pattern. is the direction i should purse or is MVVM light's messenger pattern easier to implement? I dont 
think i'd have time fit those into this assigment but maybe the final project. 
