﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
namespace Microsoft.Xaml.Interactivity
{
    using System;
    using Windows.Foundation.Collections;
    using Windows.UI.Xaml;

    /// <summary>
    /// Represents a collection of IActions.
    /// </summary>
    public sealed class ActionCollection : DependencyObjectCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCollection"/> class.
        /// </summary>
        public ActionCollection()
        {
            this.VectorChanged += this.ActionCollection_VectorChanged;
        }

        private void ActionCollection_VectorChanged(IObservableVector<DependencyObject> sender, IVectorChangedEventArgs eventArgs)
        {
            CollectionChange collectionChange = eventArgs.CollectionChange;

            if (collectionChange == CollectionChange.Reset)
            {
                foreach (DependencyObject item in this)
                {
                    ActionCollection.VerifyType(item);
                }
            }
            else if (collectionChange == CollectionChange.ItemInserted || collectionChange == CollectionChange.ItemChanged)
            {
                DependencyObject changedItem = this[(int)eventArgs.Index];
                ActionCollection.VerifyType(changedItem);
            }
        }

        private static void VerifyType(DependencyObject item)
        {
            if (!(item is IAction))
            {
                throw new InvalidOperationException(ResourceHelper.GetString("NonActionAddedToActionCollectionExceptionMessage"));
            }
        }
    }
}
