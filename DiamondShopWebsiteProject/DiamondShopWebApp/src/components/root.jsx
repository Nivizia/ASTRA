import React from "react";
import Header from "./headerAndFooter/header";
import Footer from "./headerAndFooter/footer";
import { AuthProvider } from "../contexts/AuthContext";

import styled from 'styled-components';
import { Outlet } from "react-router-dom";

const StyledContainer = styled.div
  `font-family: 'Manrope', sans-serif;`;

const MainContent = styled.main`
  flex-grow: 1;`;

const Root = () => {
  return (
    <AuthProvider>
      <StyledContainer>
        <div id="page-renderer">
          <Header />
          <MainContent>
            <Outlet />
          </MainContent>
          <Footer />
        </div>
      </StyledContainer>
    </AuthProvider>


  )
}
export default Root