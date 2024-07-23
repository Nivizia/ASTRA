// src/components/cart/orderConfirmation.jsx

import React, { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import CircularIndeterminate from '../misc/loading';

const OrderConfirmation = () => {
  const location = useLocation();
  const [orderInfo, setOrderInfo] = useState(null);

  useEffect(() => {
    const params = new URLSearchParams(location.search);

    const payDateString = params.get('vnp_PayDate');
    let payDate;
    if (payDateString && payDateString.length === 14) {
      const year = payDateString.substring(0, 4);
      const month = payDateString.substring(4, 6);
      const day = payDateString.substring(6, 8);
      const hour = payDateString.substring(8, 10);
      const minute = payDateString.substring(10, 12);
      const second = payDateString.substring(12, 14);

      // Constructing a new Date object
      const date = new Date(`${year}-${month}-${day}T${hour}:${minute}:${second}`);

      // Formatting the date to a more readable format
      payDate = new Intl.DateTimeFormat('en-US', {
        year: 'numeric', month: 'long', day: 'numeric',
        hour: 'numeric', minute: 'numeric', second: 'numeric',
        hour12: true
      }).format(date);
    } else {
      payDate = 'Invalid date';
    }

    const orderDetails = {
      amount: (Number(params.get('vnp_Amount')) / 2000000).toFixed(2),
      bankCode: params.get('vnp_BankCode'),
      bankTranNo: params.get('vnp_BankTranNo'),
      cardType: params.get('vnp_CardType'),
      orderInfo: params.get('vnp_OrderInfo'),
      payDate: payDate,
      responseCode: params.get('vnp_ResponseCode'),
      transactionStatus: params.get('vnp_TransactionStatus'),
      tmnCode: params.get('vnp_TmnCode'),
      transactionNo: params.get('vnp_TransactionNo'),
      txnRef: params.get('vnp_TxnRef'),
      secureHash: params.get('vnp_SecureHash')
    };

    setOrderInfo(orderDetails);
  }, [location.search]);

  const getTransactionStatusMessage = (status) => {
    switch (status) {
      case '00':
        return 'Giao dịch thành công';
      case '01':
        return 'Giao dịch chưa hoàn tất';
      case '02':
        return 'Giao dịch bị lỗi';
      case '04':
        return 'Giao dịch đảo (Khách hàng đã bị trừ tiền tại Ngân hàng nhưng GD chưa thành công ở VNPAY)';
      case '05':
        return 'VNPAY đang xử lý giao dịch này (GD hoàn tiền)';
      case '06':
        return 'VNPAY đã gửi yêu cầu hoàn tiền sang Ngân hàng (GD hoàn tiền)';
      case '07':
        return 'Giao dịch bị nghi ngờ gian lận';
      case '09':
        return 'GD Hoàn trả bị từ chối';
      default:
        return 'Trạng thái giao dịch không xác định';
    }
  };

  const getResponseCodeMessage = (code) => {
    switch (code) {
      case '00':
        return 'Giao dịch thành công';
      case '07':
        return 'Trừ tiền thành công. Giao dịch bị nghi ngờ (liên quan tới lừa đảo, giao dịch bất thường).';
      case '09':
        return 'Giao dịch không thành công do: Thẻ/Tài khoản của khách hàng chưa đăng ký dịch vụ InternetBanking tại ngân hàng.';
      case '10':
        return 'Giao dịch không thành công do: Khách hàng xác thực thông tin thẻ/tài khoản không đúng quá 3 lần';
      case '11':
        return 'Giao dịch không thành công do: Đã hết hạn chờ thanh toán. Xin quý khách vui lòng thực hiện lại giao dịch.';
      case '12':
        return 'Giao dịch không thành công do: Thẻ/Tài khoản của khách hàng bị khóa.';
      case '13':
        return 'Giao dịch không thành công do Quý khách nhập sai mật khẩu xác thực giao dịch (OTP). Xin quý khách vui lòng thực hiện lại giao dịch.';
      case '24':
        return 'Giao dịch không thành công do: Khách hàng hủy giao dịch';
      case '51':
        return 'Giao dịch không thành công do: Tài khoản của quý khách không đủ số dư để thực hiện giao dịch.';
      case '65':
        return 'Giao dịch không thành công do: Tài khoản của Quý khách đã vượt quá hạn mức giao dịch trong ngày.';
      case '75':
        return 'Ngân hàng thanh toán đang bảo trì.';
      case '79':
        return 'Giao dịch không thành công do: KH nhập sai mật khẩu thanh toán quá số lần quy định. Xin quý khách vui lòng thực hiện lại giao dịch';
      case '99':
        return 'Các lỗi khác (lỗi còn lại, không có trong danh sách mã lỗi đã liệt kê)';
      default:
        return 'Mã phản hồi không xác định';
    }
  };

  return (
    <div>
      <h2>Order Summary</h2>
      {orderInfo ? (
        <div>
          <p>Amount: ${orderInfo.amount}</p>
          <p>Bank: {orderInfo.bankCode}</p>
          <p>Bank Transaction Number: {orderInfo.bankTranNo}</p>
          <p>Transaction Type: {orderInfo.cardType}</p>
          <p>Description: {orderInfo.orderInfo}</p>
          <p>Payment Date: {orderInfo.payDate}</p>
          <p>Response Code: {getResponseCodeMessage(orderInfo.responseCode)}</p>
          <p>Transaction Status: {getTransactionStatusMessage(orderInfo.transactionStatus)}</p>
          <p>Terminal ID: {orderInfo.tmnCode}</p>
          <p>Transaction Number: {orderInfo.transactionNo}</p>
          <p>Transaction Reference: {orderInfo.txnRef}</p>
        </div>
      ) : (
        <CircularIndeterminate size={56}/>
      )}
    </div>
  );
};

export default OrderConfirmation;

// http://astradiamonds.com:5173/order-confirmation?vnp_Amount=1354300&vnp_BankCode=NCB&vnp_BankTranNo=VNP14527187&vnp_CardType=ATM&vnp_OrderInfo=Thanh+to%C3%A1n+cho+%C4%91%C6%A1n+h%C3%A0ng%3A+f00c1271-6d26-45b4-8b3c-db0cd21c006e&vnp_PayDate=20240723112859&vnp_ResponseCode=00&vnp_TmnCode=DUXEN1J9&vnp_TransactionNo=14527187&vnp_TransactionStatus=00&vnp_TxnRef=f00c1271-6d26-45b4-8b3c-db0cd21c006e&vnp_SecureHash=e9de2be3158091a91e450ab51025c893c6624e66b07790909925f718c771cecc50421e6cbfea53eee00fa096df1d0dbe421485329e694ee203d03f332d3cd18c