import { UserModel } from "./userModel";

export class ProfileModel extends UserModel {
    public totalAuctions: number;
    public totalBids: number;
    public totalWins: number;
}