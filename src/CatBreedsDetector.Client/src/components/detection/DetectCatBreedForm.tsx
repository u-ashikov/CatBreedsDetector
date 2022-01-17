import React from "react";
import { UploadStatus } from "../../models/enums/UploadStatus";
import ErrorsList from "../common/ErrorsList";
import { uploadImage } from "../../services/CatBreedsService";
import "../../styles/detectCatBreedForm.css";
import { ICatBreedDetectionResult } from "../../models/detection/ICatBreedDetectionResult";

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
        <form method="post" encType="multipart/form-data">
          <div className="dropbox">
            <input
              className="input-file"
              type="file"
              accept="image/*"
              name="catImage"
              onChange={(e) => this.handleChange(e)}
            />
            {this.state.uploadStatus === UploadStatus.Initial && (
              <p>
                Drag your file here to begin
                <br /> or click to browse
              </p>
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
          </div>
        </form>
        {this.state.uploadStatus === UploadStatus.Failed && (
          <div>
            <h2>Uploaded failed.</h2>
            <p>
              <a href="javascript:void(0)" onClick={this.resetForm}>
                Try again
              </a>
            </p>
          </div>
        )}
        {this.state.uploadStatus === UploadStatus.Success && (
          <div>
            <h2>File uploaded successfully.</h2>
            <p>
              <a href="javascript:void(0)" onClick={this.resetForm}>
                Upload again
              </a>
            </p>
            <h1>Your cats is: Some</h1>
            <h2>(Probability: 100%</h2>
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

  private handleChange(e: React.ChangeEvent<HTMLInputElement>): void {
    if (!e?.target?.files || e.target.files.length != 1) return;

    const imageToUpload = e.target.files[0];
    const imageUrl = URL.createObjectURL(imageToUpload);

    this.setState({
      imageToUpload: imageToUpload,
      imageUrl: imageUrl,
      uploadStatus: UploadStatus.Saving,
    });
  }

  private async uploadImage(): Promise<void> {
    this.setState({ uploadStatus: UploadStatus.Saving });

    await uploadImage(this.state.imageToUpload)
      .then((x) => {
        this.setState({ uploadStatus: UploadStatus.Success, errors: [] });
        // this.predictedBreed = x.data.breed;
        // this.predictionProbability = x.data.predictionProbability;
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

  private resetForm(): void {
    this.setState({
      imageUrl: "",
      imageToUpload: null,
      errors: [],
      uploadStatus: UploadStatus.Initial,
    });
  }
}
