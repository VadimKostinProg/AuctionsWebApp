export class CreateUserModel {
    public username: string;
    public fullName: string;
    public email: string;
    public dataOfBirth: Date;
    public image: File;
    public password: string;
    public confirmPassword: string;
}