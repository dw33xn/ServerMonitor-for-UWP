﻿using GalaSoft.MvvmLight;
using ServerMonitor.Controls;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMonitor.Models
{
    // 对应的Site表
    [Table("Site")]
    public class SiteModel : ObservableObject, INotifyPropertyChanged
    {
        int id;
        string site_name;
        string site_address;
        bool is_server;
        string protocol_type;
        int server_port;
        int monitor_interval;
        bool is_Monitor;
        int request_timecost;
        int request_count;
        string status_code;
        DateTime create_time;
        DateTime update_time;
        bool is_pre_check;
        string request_succeed_code;
        /// <summary>
        /// 协议请求结果验证的信息（如：用于测试的域名以及预期域名解析结果）
        /// </summary>
        string protocol_content;
        /// <summary>
        /// 上次请求结果(Red：0,错误)  (Orange：-1 超时) (Gray：2,未知)   (Blue：1成功)
        /// </summary>
        int is_success;
        /// <summary>
        /// 补充信息
        /// </summary>
        string others;
        /// <summary>
        /// 用于协议请求验证信息（如：用户身份验证信息）
        /// </summary>
        string protocolIdentification;
        /// <summary>
        /// 上次请求的返回内容
        /// </summary>
        string last_response;

        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id
        {
            get => id;
            set => id = value;
        }
        [NotNull, Column("site_name")]
        public string Site_name
        {
            get => site_name;
            set
            {
                site_name = value;
                //RaisePropertyChanged(() => Site_name);
            }
        }
        [NotNull, Column("is_server")]
        public bool Is_server
        {
            get => is_server;
            set
            {
                is_server = value;
                //RaisePropertyChanged(() => Is_server);
            }
        }
        [NotNull, Column("protocol_type")]
        public string Protocol_type
        {
            get => protocol_type;
            set
            {
                protocol_type = value;
                RaisePropertyChanged(() => Protocol_type);
            }
        }
        [NotNull, Column("server_port")]
        public int Server_port
        {
            get => server_port;
            set
            {
                server_port = value;
                RaisePropertyChanged(() => Server_port);
            }
        }
        [DefaultValue(value: "1", type: typeof(int)), Column("monitor_interval")]
        public int Monitor_interval
        {
            get => monitor_interval;
            set
            {
                monitor_interval = value;
                RaisePropertyChanged(() => Monitor_interval);
            }
        }
        [DefaultValue(value: "true", type: typeof(bool)), Column("is_monitoring")]
        public bool Is_Monitor
        {
            get => is_Monitor;
            set
            {
                is_Monitor = value;
                RaisePropertyChanged(() => Is_Monitor);
                //DBHelper.UpdateSite(this);
            }
        }
        [DefaultValue(value: "5000", type: typeof(int)), Column("request_timecost")]
        public int Request_TimeCost
        {
            get => request_timecost;
            set
            {
                request_timecost = value;
                RaisePropertyChanged(() => Request_TimeCost);
            }
        }
        [DefaultValue(value: "0", type: typeof(int)), Column("request_count")]
        public int Request_count
        {
            get => request_count;
            set
            {
                request_count = value;
                RaisePropertyChanged(() => Request_count);
            }
        }
        [Column("status_code")]
        public string Status_code
        {
            get => status_code;
            set
            {
                status_code = value;
                RaisePropertyChanged(() => Status_code);
            }
        }
        [Column("create_time")]
        public DateTime Create_time
        {
            get => create_time;
            set
            {
                if (!value.Equals(null))
                {
                    create_time = value;
                    RaisePropertyChanged(() => Create_time);
                }
                else
                {
                    create_time = DateTime.Now;
                    RaisePropertyChanged(() => Create_time);
                }
            }
        }
        [Column("update_time")]
        public DateTime Update_time
        {
            get => update_time;
            set
            {
                if (!value.Equals(null))
                {
                    update_time = value;
                    RaisePropertyChanged(() => Update_time);
                }
                else
                {
                    update_time = DateTime.Now;
                    RaisePropertyChanged(() => Update_time);
                }
            }
        }
        [Column("is_precheck")]
        [DefaultValue(value: "false", type: typeof(bool))]
        public bool Is_pre_check
        {
            get => is_pre_check;
            set
            {
                is_pre_check = value;
                RaisePropertyChanged(() => Is_pre_check);
            }
        }
        [Column("request_succeed_code")]
        [DefaultValue(value: "200", type: typeof(string))]
        public string Request_succeed_code
        {
            get => request_succeed_code;
            set
            {
                request_succeed_code = value;
                RaisePropertyChanged(() => Request_succeed_code);
            }
        }
        [Column("protocol_content")]
        public string Protocol_content
        {
            get => protocol_content;
            set
            {
                protocol_content = value;
                RaisePropertyChanged(() => Protocol_content);
            }
        }
        [Column("site_address")]
        [NotNull]
        public string Site_address
        {
            get => site_address;
            set
            {
                site_address = value;
                //RaisePropertyChanged(() => Site_address);
            }
        }
        [Column("is_success")]
        [DefaultValue(value: "2", type: typeof(int))]
        public int Is_success
        {
            get => is_success;
            set
            {
                is_success = value;
                RaisePropertyChanged(() => Is_success);
            }
        }
        [Column("others")]
        [DefaultValue(value: "", type: typeof(string))]
        public string Others
        {
            get => others;
            set => others = value;
        }
        [Column("protocol_identification")]
        [DefaultValue(value: "", type: typeof(string))]
        public string ProtocolIdentification
        {
            get => protocolIdentification;
            set => protocolIdentification = value;
        }
        [Column("last_response")]
        [DefaultValue(value: "", type: typeof(string))]
        public string Last_response {
            get => last_response;
            set => last_response = value;
        }
    }


}
