import React from "react";
import "styles/navItem.css";

interface INavItemProps {
  linkText: string;
  className?: string;
  onClick?: () => void;
}

export default class NavItem extends React.Component<INavItemProps> {
  public render(): JSX.Element {
    return (
      <a
        className={`nav-item ${this.props.className}`}
        onClick={this.props.onClick}
      >
        {this.props.linkText}
      </a>
    );
  }
}
