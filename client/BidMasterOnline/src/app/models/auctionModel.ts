export class AuctionModel {
    public id: string;
    public name: string;
    public startPrice: number;
    public currentBid: number;
    public endDateTime: Date;
    public imageUrls: string[];
}