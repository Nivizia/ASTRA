//React Imports
import React from 'react'
import ReactDOM from 'react-dom/client'
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";

//Component Imports
import Root from './components/root';
import Index from './components/index'

import LoginPage from './components/loginandsignup/loginPage';
import SignUp from './components/loginandsignup/signUp';

import DiamondPriceCalculator from './components/misc/diamondcalculator';

// Product Components
import DiamondList from './components/productcomponents/diamondcomponents/diamondlist';
import DiamondDetails from './components/productcomponents/diamondcomponents/diamonddetails';

import RingList from './components/productcomponents/ringcomponents/ringlist';
import RingDetails from './components/productcomponents/ringcomponents/ringdetails';

import PendantList from './components/productcomponents/pendantcomponents/pendantlist';
import PendantDetails from './components/productcomponents/pendantcomponents/pendantdetails';

// Cart and Cart-related Components
import ShoppingCart from './components/cart/shoppingcart';
import CheckOut from './components/cart/checkout';
import OrderConfirmation from './components/cart/orderConfirmation';

//Testing components
import Profile from './components/profile/profile';

//Error Component
import ErrorPage from './components/misc/errorpage';

//Css Imports
import './index.css'

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    errorElement: <ErrorPage />,
    children: [
      {
        index: true,
        element: <Index />,
        errorElement: <ErrorPage />,
      },
      {
        path: "/login/",
        element: <LoginPage />,
        errorElement: <ErrorPage />,
      },
      {
        path: "/signup/",
        element: <SignUp />,
        errorElement: <ErrorPage />,
      },
      {
        path: "/diamond/",
        element: <DiamondList />,
        errorElement: <ErrorPage />,
      },
      {
        path: "/diamond/:diamondId",
        element: <DiamondDetails />,
        errorElement: <ErrorPage />,
      },
      // Choose Diamond then Product
      {
        path: "/diamond/:diamondId/choose-ring/",
        element: <RingList />,
        errorElement: <ErrorPage />,
      },
      {
        path: "/diamond/:diamondId/choose-ring/:ringId",
        element: <RingDetails />,
        errorElement: <ErrorPage />,
      },
      {
        path: "/diamond/:diamondId/choose-pendant/",
        element: <PendantList />,
        errorElement: <ErrorPage />,
      },
      {
        path: "/diamond/:diamondId/choose-pendant/:pendantId",
        element: <PendantDetails />,
        errorElement: <ErrorPage />,
      },
      // Choose Product then Diamond
      {
        path: "/ring/",
        element: <RingList />,
        errorElement: <ErrorPage />,
      },
      {
        path: "/ring/:ringId",
        element: <RingDetails />,
        errorElement: <ErrorPage />,
      },
      {
        path: "/pendant/",
        element: <PendantList />,
        errorElement: <ErrorPage />,
      },
      {
        path: "/pendant/:pendantId",
        element: <PendantDetails />,
        errorElement: <ErrorPage />,
      },
      {
        path: "/ring/:ringId/choose-diamond/",
        element: <DiamondList />,
        errorElement: <ErrorPage />,
      },
      {
        path: "/ring/:ringId/choose-diamond/:diamondId",
        element: <DiamondDetails />,
        errorElement: <ErrorPage />,
      },
      {
        path: "/pendant/:pendantId/choose-diamond/",
        element: <DiamondList />,
        errorElement: <ErrorPage />,
      },
      {
        path: "/pendant/:pendantId/choose-diamond/:diamondId",
        element: <DiamondDetails />,
        errorElement: <ErrorPage />,
      },
      // Cart
      {
        path: "/cart/",
        element: <ShoppingCart />,
        errorElement: <ErrorPage />,
      },
      {
        path: "/checkout/",
        element: <CheckOut />,
        errorElement: <ErrorPage />,
      },
      {
        path: "/order-confirmation/",
        element: <OrderConfirmation />,
        errorElement: <ErrorPage />,
      },

      // Calculator
      {
        path: "/calculator/",
        element: <DiamondPriceCalculator />,
        errorElement: <ErrorPage />,
      },

      // Profile
      {
        path: "/profile/",
        element: <Profile />,
        errorElement: <ErrorPage />,
      }
    ]
  },
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);