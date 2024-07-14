// src/components/root.jsx

import React from "react";
import Header from "./headerAndFooter/header/header";
import Footer from "./headerAndFooter/footer/footer";
import { AuthProvider } from "../contexts/AuthContext";

import styled from 'styled-components';
import { Outlet } from "react-router-dom";

const MainContent = styled.main`
  flex-grow: 1;`;

const Root = () => {
  return (
    <AuthProvider>
      <Header />
      <MainContent>
        <Outlet />
      </MainContent>
      <Footer />
    </AuthProvider>
  )
}
export default Root;