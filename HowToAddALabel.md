# Introduction #
The following is just taken from a recent discussion on the blogger newsgroup, where it seemed not to be too easily discoverable how to add a label to blogger.

# Details #

Use this to add a label:
```
AtomCategory category = new AtomCategory();
category.Term = "labelToDisplay";
category.Scheme = "http://www.blogger.com/atom/ns#";
entry.Categories.Add(category);
```

A label is, therefore, just another category.