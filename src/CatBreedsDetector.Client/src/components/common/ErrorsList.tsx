import React from "react";

interface IErrorsListProps {
  errors: string[];
}

export default class ErrorsList extends React.Component<IErrorsListProps> {
  public render(): JSX.Element {
    return (
      this.props.errors && (
        <ul>
          {this.props.errors.map((error, index) =>
            this.renderSingleError(error, index)
          )}
        </ul>
      )
    );
  }

  private renderSingleError(error: string, index: number): JSX.Element {
    return (
      <li key={index} className="text-danger">
        <i className="fas fa-times-circle text-danger"></i> <span>{error}</span>
      </li>
    );
  }
}
