import React from "react";
import Footer from "./Footer";
import NavBar from "../navigation/NavBar";
import NavItem from "../navigation/NavItem";
import Home from "./Home";
import DetectCatBreedForm from "../detection/DetectCatBreedForm";
import About from "./About";
import "../../styles/main.css";

interface ILayoutState {
  displayHome: boolean;
  displayDetectBreedForm: boolean;
  displayAbout: boolean;
}

export default class Layout extends React.Component<{}, ILayoutState> {
  public constructor(props: any) {
    super(props);

    this.state = {
      displayHome: false,
      displayDetectBreedForm: false,
      displayAbout: false,
    };
  }

  public render(): JSX.Element {
    return (
      <div id="app" className="container">
        <img
          className="img-thumbnail rounded-circle mt-3"
          src="/cat-paw-logo.png"
        />
        <h1 className="display-3 mt-3">Cat Breeds Detector</h1>
        <hr />
        <div className="row">
          <NavBar>
            <NavItem linkText="Home" onClick={() => this.shouldDisplayHome()} />
            <NavItem
              linkText="Detect Breed"
              onClick={() => this.shouldDisplayDetectBreedForm()}
            />
            <NavItem
              linkText="About"
              onClick={() => this.shouldDisplayAbout()}
            />
          </NavBar>
          <div className="col-9">
            {this.state.displayHome && <Home />}
            {this.state.displayDetectBreedForm && <DetectCatBreedForm />}
            {this.state.displayAbout && <About />}
          </div>
        </div>
        <Footer />
      </div>
    );
  }

  private shouldDisplayHome(): void {
    if (this.state.displayHome) return;

    this.setState({
      displayHome: !this.state.displayHome,
      displayAbout: false,
    });
  }

  private shouldDisplayAbout(): void {
    if (this.state.displayAbout) return;

    this.setState({
      displayAbout: !this.state.displayAbout,
      displayHome: false,
    });
  }

  private shouldDisplayDetectBreedForm(): void {
    if (this.state.displayDetectBreedForm) return;

    this.setState({
      displayDetectBreedForm: !this.state.displayDetectBreedForm,
      displayHome: false,
      displayAbout: false,
    });
  }
}
