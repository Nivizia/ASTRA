import React, { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import CircularIndeterminate from '../misc/loading';
import { sendPaymentResponse } from '../../../javascript/apiService';
import styles from '../css/orderConfirmation.module.css';

const OrderConfirmation = () => {
  const location = useLocation();
  const [orderInfo, setOrderInfo] = useState(null);

  useEffect(() => {
    const params = new URLSearchParams(location.search);

    const formatPayDate = (dateString) => {
      if (dateString.length === 14) {
        const year = dateString.substring(0, 4);
        const month = dateString.substring(4, 6);
        const day = dateString.substring(6, 8);
        const hour = dateString.substring(8, 10);
        const minute = dateString.substring(10, 12);
        const second = dateString.substring(12, 14);
        return `${year}-${month}-${day}T${hour}:${minute}:${second}`;
      }
      return null;
    };

    const orderDetails = {
      orderId: params.get('vnp_TxnRef'),
      responseCode: params.get('vnp_ResponseCode'),
      transactionStatus: params.get('vnp_TransactionStatus'),
      amount: (Number(params.get('vnp_Amount')) / 2000000).toFixed(2),
      bankCode: params.get('vnp_BankCode'),
      bankTranNo: params.get('vnp_BankTranNo'),
      cardType: params.get('vnp_CardType'),
      orderInfo: params.get('vnp_OrderInfo'),
      paymentDate: formatPayDate(params.get('vnp_PayDate')),
      date: params.get('vnp_PayDate'),
      //tmnCode: params.get('vnp_TmnCode'),
      //transactionNo: params.get('vnp_TransactionNo'),
      //secureHash: params.get('vnp_SecureHash')
    };

    setOrderInfo(orderDetails);
    sendPaymentResponse(orderDetails);
  }, [location.search]);
  /*
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
    */

  function getPayDate(payDateString) {
    let payDate;
    if (payDateString && payDateString.length === 14) {
      const year = payDateString.substring(0, 4);
      const month = payDateString.substring(4, 6);
      const day = payDateString.substring(6, 8);
      const hour = payDateString.substring(8, 10);
      const minute = payDateString.substring(10, 12);
      const second = payDateString.substring(12, 14);

      const date = new Date(`${year}-${month}-${day}T${hour}:${minute}:${second}`);

      payDate = new Intl.DateTimeFormat('en-US', {
        year: 'numeric', month: 'long', day: 'numeric',
        hour: 'numeric', minute: 'numeric', second: 'numeric',
        hour12: true
      }).format(date);
    } else {
      payDate = 'Invalid date';
    }
    return payDate;
  }

  return (
    <div className={styles.mainContainer}>
      <h2 className={styles.title}>Order Summary</h2>
      {orderInfo ? (
        <div className={styles.detailsContainer}>
          {orderInfo.responseCode === '00' && orderInfo.transactionStatus === '00' ? (
            <p className={styles.successMessage}>Thank you for your purchase. Your order has been successfully processed.</p>
          ) : (
            <p className={styles.errorMessage}>There was an error processing your order. Please try again later.</p>
          )}
          <div className={styles.detail}>
            <span className={styles.detailLabel}>OrderId:</span>
            <span className={styles.detailValue}>{orderInfo.orderId}</span>
          </div>
          {orderInfo.amount && (
            <div className={styles.detail}>
              <span className={styles.detailLabel}>Amount:</span>
              <span className={styles.detailValue}>${orderInfo.amount}</span>
            </div>
          )}
          {orderInfo.bankCode && (
            <div className={styles.detail}>
              <span className={styles.detailLabel}>Bank:</span>
              <span className={styles.detailValue}>{orderInfo.bankCode}</span>
            </div>
          )}
          {orderInfo.bankTranNo && (
            <div className={styles.detail}>
              <span className={styles.detailLabel}>Bank Transaction Number:</span>
              <span className={styles.detailValue}>{orderInfo.bankTranNo}</span>
            </div>
          )}
          {orderInfo.cardType && (
            <div className={styles.detail}>
              <span className={styles.detailLabel}>Transaction Type:</span>
              <span className={styles.detailValue}>{orderInfo.cardType}</span>
            </div>
          )}
          {orderInfo.orderInfo && (
            <div className={styles.detail}>
              <span className={styles.detailLabel}>Description:</span>
              <span className={styles.detailValue}>{orderInfo.orderInfo}</span>
            </div>
          )}
          {orderInfo.paymentDate && (
            <div className={styles.detail}>
              <span className={styles.detailLabel}>Payment Date:</span>
              <span className={styles.detailValue}>{getPayDate(orderInfo.date)}</span>
            </div>
          )}
        </div>
      ) : (
        <CircularIndeterminate size={56} />
      )}
    </div>
  );
};

export default OrderConfirmation;