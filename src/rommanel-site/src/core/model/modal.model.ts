export class ModalModel {
  Message?: string;
  Title?: string;

  constructor(message: string, title: string) {
    this.Message = message;
    this.Title = title;
  }
}
