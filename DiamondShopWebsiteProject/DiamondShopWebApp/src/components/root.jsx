import Header from "./header"
import Footer from "./footer"
import styled from 'styled-components';

const StyledContainer = styled.div
  `font-family: 'Manrope', sans-serif;`;

const Root = () => {
  return (
    <StyledContainer>
      <div id="page-renderer">
        <Header />
        <main>
          <div>
            haro
          </div>
          <div>
            haro
          </div>
          <div>
            haro
          </div>
          <div>
            haro
          </div>
          <div>
            haro
          </div>
          <div>
            haro
          </div>
          <div>
            haro
          </div>
          <div>
            haro
          </div>
          <div>
            haro
          </div>
          <div>
            haro
          </div>
          <div>
            haro
          </div>
        </main>
        <Footer />
      </div>
    </StyledContainer>


  )
}
export default Root