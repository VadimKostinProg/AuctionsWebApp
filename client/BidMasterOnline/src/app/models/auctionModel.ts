export class AuctionModel {
    public id: string;
    public name: string;
    public category: string;
    public startPrice: number;
    public currentBid: number;
    public endDateTime: Date;
    public imageUrl: string;
}