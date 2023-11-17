export interface AuctionModel {
    id: string;
    name: string;
    category: string;
    startPrice: number;
    currentBid: number;
    endDateTime: Date;
    imageUrl: string;
}