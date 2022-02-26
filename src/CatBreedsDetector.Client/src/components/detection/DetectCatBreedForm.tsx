import React from "react";
import { UploadStatus } from "models/enums/UploadStatus";
import ErrorsList from "components/common/ErrorsList";
import { uploadImage } from "services/CatBreedsService";
import "styles/detectCatBreedForm.css";
import { ICatBreedDetectionResult } from "models/detection/ICatBreedDetectionResult";
import CatBreedPrediction from "components/detection/CatBreedPrediction";

interface IDetectCatBreedFormProps {}

interface IDetectCatBreedFormState {
  imageToUpload: File;
  catBreedPredictionResult?: ICatBreedDetectionResult;
  imageUrl?: string;
  uploadStatus: UploadStatus;
  errors: string[];
}

export default class DetectCatBreedForm extends React.Component<
  IDetectCatBreedFormProps,
  IDetectCatBreedFormState
> {
  public constructor(props: IDetectCatBreedFormProps) {
    super(props);

    this.state = {
      imageToUpload: null,
      uploadStatus: UploadStatus.Initial,
      errors: [],
    };
  }

  public render(): JSX.Element {
    return (
      <>
        {this.state.uploadStatus === UploadStatus.Initial && (
          <form method="post" encType="multipart/form-data">
            <div className="dropbox">
              <input
                className="input-file"
                type="file"
                accept="image/*"
                name="catImage"
                onChange={(e) => this.handleImageChange(e)}
              />
              <p>
                Drag your file here to begin
                <br /> or click to browse
              </p>
            </div>
          </form>
        )}
        {this.state.uploadStatus === UploadStatus.Saving && (
          <div>
            <p>Uploading the file...</p>
            <img
              src={this.state.imageUrl}
              className="img-responsive img-thumbnail"
              alt={this.state.imageToUpload?.name}
            />
          </div>
        )}
        {this.state.uploadStatus === UploadStatus.Failed && (
          <div>
            <h2>Uploaded failed.</h2>
            <p>
              <a className="inactive-link" onClick={this.resetForm}>
                Try again
              </a>
            </p>
          </div>
        )}
        {this.state.uploadStatus === UploadStatus.Success && (
          <div>
            <h2>File uploaded successfully.</h2>
            <p>
              <a className="inactive-link" onClick={this.resetForm}>
                Upload again
              </a>
            </p>
            <CatBreedPrediction
              prediction={this.state.catBreedPredictionResult}
            />
            <img
              src={this.state.imageUrl}
              className="img-responsive img-thumbnail"
            ></img>
          </div>
        )}
        <ErrorsList errors={this.state.errors} />
      </>
    );
  }

  private async handleImageChange(
    e: React.ChangeEvent<HTMLInputElement>
  ): Promise<void> {
    if (!e?.target?.files || e.target.files.length != 1) return;

    const imageToUpload = e.target.files[0];
    const imageUrl = URL.createObjectURL(imageToUpload);

    this.setState({
      imageToUpload: imageToUpload,
      imageUrl: imageUrl,
      uploadStatus: UploadStatus.Saving,
    });

    await this.uploadImage(imageToUpload);
  }

  private async uploadImage(imageToUpload: File): Promise<void> {
    this.setState({ uploadStatus: UploadStatus.Saving });

    await uploadImage(imageToUpload)
      .then((response) => {
        this.setState({
          uploadStatus: UploadStatus.Success,
          catBreedPredictionResult: response?.data,
          errors: [],
        });
      })
      .catch((error) => {
        if (error && error.response.status == 400) {
          var errorsArray = error.response.data.map(function (e: any) {
            return e.errors;
          })[0];

          var allErrorMessages = errorsArray.map(function (e: any) {
            return e.errorMessage;
          });

          this.setState({ errors: allErrorMessages });
        }

        this.setState({ uploadStatus: UploadStatus.Failed });
      });
  }

  private resetForm = (): void => {
    this.setState({
      imageUrl: null,
      imageToUpload: null,
      errors: [],
      uploadStatus: UploadStatus.Initial,
    });
  };
}
