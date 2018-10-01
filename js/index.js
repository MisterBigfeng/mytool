

const menuConfig=getAllMenuConfig();
var tabsId = 1;

tabsId = parseInt(getUrlParam("tabsId"));
var app = new Vue({
    data: function () {
        return {
            activeName: 'first',
            visible: false,
            menuConfig: menuConfig,
            featruesList: [{ID:'',demandName:""}],
            testCaseList: [],
            caseList: [],
            uploadUrl: "",
            fileList3: [],
            operateType: undefined,
            ruleForm: {
                ID: '',
                demandID: '',
                demandName: '',
                functionModule: '',
                functionPoint: '',
                caseID: '',
                caseCode: '',
                caseName: '',
                precondition: '',
                caseType: '',
                caseNature: '',
                caseDesc: '',
                expectedResult: '',
                priority: '',
                caseEditOP: '',
                caseOP: '',
                actualResult: '',
                caseRemarks: '',
                casePic: ''
            },
            caseNatureList: [
                {
                    value: '正案例',
                    label: '正案例'
                }, {
                    value: '反案例',
                    label: '反案例'
                }
            ],
            caseTypeList: [
                {
                    value: '页面',
                    label: '页面'
                }, {
                    value: '功能',
                    label: '功能'
                },
                    {
                        value: '流程',
                        label: '流程'
                    }
            ],
            caseOPList: [
                {
                    value: '王翊武',
                    label: '王翊武'
                }, {
                    value: '赵成旭',
                    label: '赵成旭'
                }, {
                    value: '龙茂莎',
                    label: '龙茂莎'
                }, {
                    value: '陈亚男',
                    label: '陈亚男'
                }, {
                    value: '刘美兰',
                    label: '刘美兰'
                }, {
                    value: '钱佳磊',
                    label: '钱佳磊'
                }, {
                    value: '吴茜茜',
                    label: '吴茜茜'
                }, {
                    value: '丰文勇',
                    label: '丰文勇'
                }
            ],
            actualResultList: [
                {
                    value: 'Norun',
                    label: 'Norun'
                },
                {
                    value: 'Passed',
                    label: 'Passed'
                },
                {
                    value: 'Failed',
                    label: 'Failed'
                },
                {
                    value: 'Blocked',
                    label: 'Blocked'
                }
            ], priorityList: [
                {
                    value: '低',
                    label: '低'
                },
                {
                    value: '中',
                    label: '中'
                },
                {
                    value: '高',
                    label: '高'
                }
            ],
            rules: {
                demandName: [
                    { required: true, message: '请点击新增案例', trigger: 'blur' }
                ],
                caseCode: [
                    { required: true, message: '请输入案例编号', trigger: 'blur' }
                ],
                caseName: [
                    { required: true, message: '请输入案例名称', trigger: 'blur' }
                ],
                functionModule: [
                    { required: true, message: '请输入功能模块', trigger: 'blur' }
                ],
                functionPoint: [
                    { required: true, message: '请输入功能点', trigger: 'blur' }
                ],
                caseNature: [
                    { required: true, message: '请选择案例性质', trigger: 'change' }
                ],
                caseType: [
                    { required: true, message: '请选择案例类型', trigger: 'change' }
                ],
                caseDescription: [
                    { required: true, message: '请输入案例描述', trigger: 'blur' }
                ],
                expectedResult: [
                    { required: true, message: '请输入预期结果', trigger: 'blur' }
                ],
                caseOP: [
                    { required: true, message: '请选择案例执行人', trigger: 'change' }
                ],
                actualResult: [
                    { required: true, message: '请选择实际结果', trigger: 'change' }
                ]
            },
            getRowKeys:function(row) {// 获取row的key值
                return row.rowIndex;
            },
            // 要展开的行，数值的元素是row的key值
            expands: [],
            demandPageSize: 15,
            demandPageIndex: 1,
            demandTotalRows: 0,
            demandTotalPages: 0,
            casePageSize: 15,
            casePageIndex: 1,
            caseTotalRows: 0,
            caseTotalPages: 0,
            forumPageIndex: 1,
            forumPageSize: 30,
            forumTotalRows: 0,
            forumTotalPages: 0,
            defectPageIndex: 1,
            defectPageSize: 10,
            defectTotalRows: 0,
            defectTotalPages: 0,
            loading1: false,
            downloadurl: "#",
            caseIDQuery: '',
            caseNameQuery: '',
            rowIndex: undefined,
            show: false,
            multipleSelection: [],
            forumList: [
            ],
            operateNo: undefined,
            forum: {
                cardGroupName: '',
                cardName:''
            },
            forumForm: {
                ID:'',
                postGroupName: '',
                postName: '',
                postContent: '',
                postOP: '',
                opType:''
            },
            forums: {

            },
            demandList:[],
            demandInfo: {
                status: ""
                , demandID: ""
                , demandName: ""
                , kjb: ""
                , devOP: ""
                , testOP: ""
                , sitTestTime: ""
                , sitComplatetime: ""
                , estmateComplateTime:""
                , uatFinishDatetime: ""
                , uatTestDatetime:""
                , actualComplateTime: ""
                , submitConfirmTime: ""
                , estmateProductionTime: ""
                , uatTestOP: ""
                , bankOP: ""
                , demandType: ""
                , Totaltestcase: ""
                , Totaltestcasepass: ""
                , Todaytestcasepass: ""
                , Totalbug: ""
                , Notrepairedbug: ""
                , remark: ""

            },
            demands:{
                demandID: ''
                ,demandName:''
            },
            sessionName:undefined,
            dialogFormVisible: false,
            defectList: [],
            defectInfo: {
                status: ''
                , defectNo: ''
                , defectName: ''
                , defectGrade: ''
                , demandID: ''
                , defectType: ''
                , caseID: ''
                , defectDescription: ''
                , finishDatetime: ''
                , accessory: ''
                , remark: ''
                , functionModule: ''
                , functionPoint: ''
                , yzcd: ''
                , devOP: ''
                , testOP: ''
                , defectSource: ''
                , defectFX: ''
                , findDatetime: ''
            },
            defects: {

            }
        }
    },
    methods: {
        tabClick: function (tab, event) {

            if (tab.index == 0) {
                this.getTestCaseList(1);
            }
            if (tab.index == 1) {
                this.getDemandList(1);
            }
            if (tab.index == 2) {
                this.getDefectList(1);
            }
            if (tab.index == 3) {
                this.getForumList(1);
            }
        },
        //获取案例板块需求列表  测试案例板块
        getTestCaseList: function (pageIndex) {
            app.ruleForm.caseNature = "正案例";
            app.ruleForm.caseType = "页面";
            app.ruleForm.actualResult = "Norun";
            app.ruleForm.priority = "低";

            app.loading1 = true;
            var jsonStr = {
                id: 0,
                type: 1,  //type:1 获取  2：添加 3：编辑
                demandID: this.caseIDQuery,
                demandName: this.caseNameQuery,
                pageIndex: pageIndex,
                pageSize: this.casePageSize
            };

            getJSON("Handler/TestCaseMsg.ashx", jsonStr).then(function (res) {

                app.loading1 = false;

                if (res.success == "-1") {
                    window.location.href = "login.html";
                    return false;
                }

                app.testCaseList = res.dataList;
                app.caseTotalRows = res.totalRows;
                app.caseTotalPage = res.totalPage;
                //app.ruleForm.caseOP = res.sessionName;
            });
        },
        //新增案例信息 提交按钮
        submitForm: function (formName) {
            var flag = false;

            this.$refs[formName].validate(function (valid) { if (valid) { flag = true } });
            if (!flag) {
                app.$message.error("* 号为必填项!");
                return false;
            }
            else {
                var obj = {};

                obj.ID = app.ruleForm.ID;
                obj.demandID = app.ruleForm.demandID;
                obj.demandName = app.ruleForm.demandName;
                obj.caseID = app.ruleForm.caseID;
                obj.caseCode = app.ruleForm.caseCode;
                obj.caseName = app.ruleForm.caseName;
                obj.functionModule = app.ruleForm.functionModule;
                obj.functionPoint = app.ruleForm.functionPoint;
                obj.caseNature = app.ruleForm.caseNature;
                obj.caseType = app.ruleForm.caseType;
                obj.caseDescription = app.ruleForm.caseDescription;


                obj.expectedResult = app.ruleForm.expectedResult;
                obj.caseOP = app.ruleForm.caseOP;
                obj.actualResult = app.ruleForm.actualResult;
                obj.caseRemarks = app.ruleForm.caseRemarks;
                obj.precondition = app.ruleForm.precondition;
                obj.priority = app.ruleForm.priority;
                if (app.ruleForm.caseRemarks == undefined || app.ruleForm.caseRemarks == "") {
                    obj.caseRemarks = "0";
                }
                else {

                    obj.caseRemarks = app.ruleForm.caseRemarks;
                }

                if (app.ruleForm.casePic == undefined || app.ruleForm.casePic == "") {
                    obj.casePic = "0";
                }
                else {

                    obj.casePic = app.ruleForm.casePic;
                }

                obj.operateType = app.operateType;

                if (app.operateType == undefined) {

                    app.$message.error("请先点击 新增案例!");

                    return false;
                }

                obj.type = 4;//创建

                getJSON("Handler/TestCaseMsg.ashx", obj).then(function (res) {

                    if (res.success == "True") {

                        app.$message.success("提交成功!");
                        app.updateCaseList(obj.demandID, obj.ID, app.rowIndex);

                    }
                    else {
                        app.$message.error(res.errorMsg);
                    }
                });
            }
        },
        //清空文本
        resetForm: function (formName) {
            this.$refs[formName].resetFields();
        },
        /**
         * 点击行事件
         * @param row
         */
        tableRowClick: function (row) {

            //展开 折叠
            if (app.rowIndex == row.rowIndex) {
                if (app.expands.length > 0) {
                    app.expands.pop();
                    return false;
                }
            }


            app.updateCaseList(row.demandID, row.ID, row.rowIndex);

            app.rowIndex = row.rowIndex;

            app.addCaseInfo(row.ID, row.demandID, row.demandName);

            app.ruleForm.demandID = row.demandID;
            app.ruleForm.demandName = row.demandName;
            app.ruleForm.ID = row.ID;

        },
        expandClick: function () {
            alert(0);
        },
        /**
         * 更新案例信息
         * @param demandID
         * @param ID
         * @param rowIndex
         */
        updateCaseList: function (demandID, ID, rowIndex) {

            var obj = {};
            obj.demandID = demandID;
            obj.ID = ID;
            obj.type = 2;

            getJSON("Handler/TestCaseMsg.ashx", obj).then(function (res) {

                app.loading1 = false;

                app.caseList = res.dataList;

                app.expands.pop();

                app.expands.push(app.testCaseList[rowIndex - 1].rowIndex);


            });
        },
        /**
         * 新增案例
         * @param demandID
         * @param demandName
         */
        addCaseInfo: function (ID, demandID, demandName) {

            app.ruleForm.ID = ID;
            app.ruleForm.demandID = demandID;
            app.ruleForm.demandName = demandName;

            app.ruleForm.caseID = '';
            app.ruleForm.caseCode = '';
            app.ruleForm.caseName = '';
            app.ruleForm.functionModule = '';
            app.ruleForm.functionPoint = '';
            //app.ruleForm.caseNature = '';
            app.ruleForm.caseType = '';
            app.ruleForm.caseDescription = '';
            app.ruleForm.expectedResult = '';
            app.ruleForm.caseOP = '';
            app.ruleForm.caseRemarks = '';
            app.ruleForm.casePic = '';
            app.ruleForm.precondition = '';
            app.ruleForm.priority = '';
            app.operateType = 'add'
            this.operateNo = 0;//操作编号 新增

        },
        /**
         * 复制案例
         * @param demandID
         * @param demandName
         * @param ID
         * @param caseID
         */
        copyCaseInfo: function (demandID, demandName, ID, caseID) {

            var obj = {};
            obj.caseID = caseID;
            obj.type = 3;//编辑

            getJSON("Handler/TestCaseMsg.ashx", obj).then(function (res) {

                app.ruleForm.caseID = res.dataList[0].ID;
                app.ruleForm.caseCode = res.dataList[0].caseCode;
                app.ruleForm.caseName = res.dataList[0].caseName;
                app.ruleForm.functionModule = res.dataList[0].functionModule;
                app.ruleForm.functionPoint = res.dataList[0].functionPoint;
                app.ruleForm.caseNature = res.dataList[0].caseNature;
                app.ruleForm.caseType = res.dataList[0].caseType;
                app.ruleForm.caseDescription = res.dataList[0].caseDescription.replace(/\^/g, "\r\n");
                app.ruleForm.expectedResult = res.dataList[0].expectedResult.replace(/\^/g, "\r\n");
                app.ruleForm.caseOP = res.dataList[0].caseOP;
                app.ruleForm.actualResult = res.dataList[0].actualResult;

                app.ruleForm.caseRemarks = res.dataList[0].caseRemarks;
                app.ruleForm.casePic = res.dataList[0].casePic;
                app.ruleForm.precondition = res.dataList[0].precondition.replace(/\^/g, "\r\n");
                app.ruleForm.priority = res.dataList[0].priority;
            });

            app.ruleForm.ID = ID;
            app.ruleForm.demandID = demandID;
            app.ruleForm.demandName = demandName;

            this.operateType = "add";
            this.operateNo = 1;//操作编号 复制

        },
        /**
         * 编辑案例
         * @param demandID
         * @param demandName
         * @param ID
         * @param caseID
         */
        editCaseInfo: function (row) {

            var obj = {};
            obj.caseID = row.ID;
            obj.type = 3;//编辑

            getJSON("Handler/TestCaseMsg.ashx", obj).then(function (res) {

                app.ruleForm.caseID = res.dataList[0].ID;
                app.ruleForm.caseCode = res.dataList[0].caseCode;
                app.ruleForm.caseName = res.dataList[0].caseName;
                app.ruleForm.functionModule = res.dataList[0].functionModule;
                app.ruleForm.functionPoint = res.dataList[0].functionPoint;
                app.ruleForm.caseNature = res.dataList[0].caseNature;
                app.ruleForm.caseType = res.dataList[0].caseType;
                app.ruleForm.caseDescription = res.dataList[0].caseDescription.replace(/\^/g, "\r\n");
                app.ruleForm.expectedResult = res.dataList[0].expectedResult.replace(/\^/g, "\r\n");
                app.ruleForm.caseOP = res.dataList[0].caseOP;
                app.ruleForm.actualResult = res.dataList[0].actualResult;
                app.ruleForm.caseRemarks = res.dataList[0].caseRemarks;
                app.ruleForm.casePic = res.dataList[0].casePic;
                app.ruleForm.precondition = res.dataList[0].precondition.replace(/\^/g, "\r\n");
                app.ruleForm.priority = res.dataList[0].priority;

            });

            this.operateType = "edit";
            this.operateNo = 2;//操作编号 编辑

        },
        /**
         * 删除案例信息
         * @param demandID
         * @param demandName
         * @param ID
         * @param caseID
         */
        deleteoffsetWidthCaseInfo: function (demandID, demandName, ID, caseID) {
            var r = confirm("确定删除？")
            if (r == true) {
                var obj = {};
                obj.caseID = caseID;
                obj.type = 5;//编辑
                getJSON("Handler/TestCaseMsg.ashx", obj).then(function (res) {

                    if (res.success == "True") {

                        app.updateCaseList(demandID, ID, app.rowIndex);

                        app.$message.success("删除成功!");

                    }
                    else {
                        app.$message.error("删除失败!");
                    }

                });

            }
            else {
                return false;
            }

        },
        /**
         * 导入需求
         */
        importDemandExcel: function () {
            app.uploadUrl = "Handler/ImportFile.ashx?type=1&filers";

            $("#uploadFileId").click();
        },
        /**
         * 导入案例
         * @param demandID
         */
        importCaseExcel: function (demandID, ID) {
            app.uploadUrl = "Handler/ImportFile.ashx?type=2&demandID=" + demandID + "&ID=" + ID + "&filers";

            app.ruleForm.ID = ID;
            app.ruleForm.demandID = demandID;

            $("#uploadFileId").click();
        },
        /**
         * 上传事件发生时
         * @param file
         * @param fileList
         */
        uploadChange: function (file, fileList) {
            fileList.splice(0, fileList.length - 1);

            app.uploadUrl = "Handler/ImportFile.ashx?type=1&filers";

        },
        /**
         * 上传成功
         */
        uploadSuccess: function () {

            app.$message.success("导入成功!");

            app.updateCaseList(app.ruleForm.demandID, app.ruleForm.ID, app.rowIndex);

            app.loading1 = false;
        },
        handleSelectionChange: function (val) {

            this.multipleSelection = val;

            var arrayData = "";

            if (val.length > 0) {

                for (var i = 0; i < val.length; i++) {
                    if (i == val.length - 1) {
                        arrayData += val[i].ID;
                    }
                    else {
                        arrayData += val[i].ID + "|";
                    }
                }
            }

            app.downloadurl = "Handler/ExportFile.ashx?arrayData=" + arrayData;

        },
        submitUpload: function () {
            this.$refs.upload.submit();
        },
        demandSizeChange: function (val) {
            this.demandPageIndex = val;
            this.getDemandList(val);
        },
        demandCurrentChange: function (val) {
            this.demandPageIndex = val;
            this.getDemandList(val);
        },
        caseSizeChange: function (val) {
            this.casePageIndex = val;
            this.getTestCaseList(val);
        },
        caseCurrentChange: function (val) {
            this.casePageIndex = val;
            this.getTestCaseList(val);
        },
        forumSizeChange: function (val) {
            this.forumPageIndex = val;
            this.getForumList(val);
        },
        forumCurrentChange: function (val) {
            this.forumPageIndex = val;
            this.getForumList(val);
        },
        defectSizeChange: function (val) {
            this.defectPageIndex = val;
            this.getDefectList(val);
        },
        defectCurrentChange: function (val) {
            this.defectPageIndex = val;
            this.getDefectList(val);
        },
        /**
         * 退出登陆
         */
        exitLogin: function () {
            var jsonStr = {
                userName: '',
                pwd: '',
                type: -1  //type:1 登录  -1：退出
            };
            getJSON("Handler/UserMsg.ashx", jsonStr).then(function (res) {

                if (res.success == "True") {

                    window.location.href = "login.html";
                }
            });
        },
        /**
         * 需求迁移
         */
        dataMigration: function () {
            getJSON("Handler/dataMigration.ashx", null).then(function (res) {

                if (res.success == "True") {

                    app.$message.success("迁移成功!");
                    app.getDemandList(1);
                }
            });
        },
        /**
         * 案例迁移
         */
        dataMigrationCase: function () {
            getJSON("Handler/dataMigrationCase.ashx", null).then(function (res) {

                if (res.success == "True") {

                    app.$message.success("迁移成功!");
                    app.getDemandList(1);
                }
            });

        },
        //获取网银论坛列表
        getForumList: function (pageIndex) {

            var _self = this;

            var obj = {};
            obj.type = 1;
            obj.postName = _self.forum.cardName;
            obj.postGroupName = _self.forum.cardGroupName;
            obj.pageIndex = pageIndex,
            obj.pageSize = _self.forumPageSize

            getJSON("Handler/ForumMsg.ashx", obj).then(function (res) {

                if (res.success == "-1") {
                    window.location.href = "login.html";
                    return false;
                }


                _self.forumList = res.Table;
                _self.sessionName = res.sessionName;
                _self.forumPageSize = res.Table1[0].pageSize;
                _self.forumTotalRows = res.Table1[0].totalRows;
                _self.forumTotalPages = res.Table1[0].totalPages;
            });
        },
        //论坛详情
        postDetails: function (row) {

            window.open("/post.html?id=" + row.ID);
        },
        deletePostInfo: function (id) {
            var obj = {};
            obj.startPostID = id;
            obj.type = 7;

            var r = confirm("确定删除？")
            if (r == true) {
                getJSON("Handler/ForumMsg.ashx", obj).then(function (res) {

                    app.getForumList(1);
                });
            }
        },
        editPostInfo: function (row) {

            var _self = this;

            _self.forumForm.ID = row.ID;

            //_self.forumForm.postGroupName = row.postGroupName;

            //_self.forumForm.postName = row.startPostName;

            //_self.forumForm.postContent = row.spartPostContent;

            //_self.forumForm.postOP = row.startPostOP;

            //_self.forumForm.opType = 6;

            //$(".el-dialog.el-dialog--small").css({ "top": "5%" });


            window.open('editor.html?id=' + row.ID + '&isEdit=1', '_black')

        },
        //发帖
        sendPost: function () {
            //var _self = this;
            //_self.forumForm.opType = 3;
            //_self.forumForm.postName = "";
            //_self.forumForm.postGroupName = "";
            //_self.forumForm.postContent = "";
            //$(".el-dialog.el-dialog--small").css({ "top": "5%" });

            window.open('editor.html?id=0&isEdit=0', '_black')
            
        },
        //保存论坛
        submitPost: function () {
            var _self = this;
            var obj = {};
            obj.startPostID = _self.forumForm.ID;
            obj.postName = _self.forumForm.postName;
            obj.postGroupName = _self.forumForm.postGroupName;
            if (_self.forumForm.postGroupName == "开发论坛") {
                obj.postGroupName = 1;
            }
            else if (_self.forumForm.postGroupName == "测试论坛") {
                obj.postGroupName = 2;
            }
            else if (_self.forumForm.postGroupName == "灌水杂谈") {
                obj.postGroupName = 3;
            }
            else if (_self.forumForm.postGroupName == "测试数据") {
                obj.postGroupName = 4;
            }
            obj.postContent = _self.forumForm.postContent;

            obj.type = _self.forumForm.opType;

            var r = confirm("您确定提交吗？")
            if (r == true) {
                getJSON("handler/forummsg.ashx", obj).then(function (res) {

                    app.getForumList(1);
                    app.dialogFormVisible = false;
                });
            }
        },
        //获取需求信息
        getDemandList: function (pageIndex) {

            var _self = this;

            var jsonStr = {
                id: 0,
                type: 1,  //type:1 获取  2：添加 3：编辑
                demandID: app.demands.demandID,
                demandName: app.demands.demandName,
                pageIndex: pageIndex,
                pageSize: app.demandPageSize
            };

            getJSON("Handler/DemandMsg.ashx", jsonStr).then(function (res) {

                if (res.success == "-1") {
                    window.location.href = "login.html";
                    return false;
                }
                _self.demandList = res.dataList;

                app.demandTotalRows = res.totalRows;
                app.demandTotalPage = res.totalPage;
            });

        },
        //保存需求信息
        saveDemandInfo: function () {

            var _self = this;
            var obj = Object.assign({}, app.demandInfo);

            obj.type = 2;

            var r = confirm("确定添加该需求？")
            if (r == true) {
                getJSON("Handler/DemandMsg.ashx", obj).then(function (res) {

                    app.getDemandList(1);
                });
            }
            else {
                return false;
            }
        },
        //删除需求信息
        deleteDemandInfo: function (demandID, demandName, ID) {
            var _self = this;
            var obj = {}
            obj.demandID = demandID;
            obj.demandName = demandName;
            obj.ID = ID;

            obj.type = 3;

            var r = confirm("确定删除？")
            if (r == true) {

                getJSON("Handler/DemandMsg.ashx", obj).then(function (res) {

                    app.getDemandList(1);
                });

            }
            else {
                return false;
            }
        },
        getContent: function (row) {
            console.log(row);

            var _self = this;

            var obj = {};
            obj.ID = row.ID;
            obj.demandID = row.demandID;
            obj.demandName = row.demandName;
            obj.type = 4;
            getJSON("Handler/DemandMsg.ashx", obj).then(function (res) {
                console.log(res)
                var dataList = res.dataList[0];
                _self.demandInfo.status = dataList.status
                _self.demandInfo.demandName = dataList.demandName
                _self.demandInfo.kjb = dataList.bankOP
                _self.demandInfo.bankOP = dataList.bankOP
                _self.demandInfo.devOP = dataList.devOP
                _self.demandInfo.testOP = dataList.testOP
                _self.demandInfo.sitTestTime = dataList.sitTestTime
                _self.demandInfo.sitComplatetime = dataList.sitComplatetime
                _self.demandInfo.estmateComplateTime = dataList.estmateComplateTime
                _self.demandInfo.estmateProductionTime = dataList.estmateProductionTime
                _self.demandInfo.uatTestDatetime = dataList.uatTestDatetime
                _self.demandInfo.uatFinishDatetime = dataList.uatFinishDatetime
                _self.demandInfo.demandType = dataList.demandType
            });

        },
        getDefectList: function (pageIndex) {
            var _self = this;

            var obj = {};
            obj.type = 1;//获取列表
            obj.defectName = "";
            obj.defectNo = "";
            obj.demandID = "";
            obj.pageSize = 10;
            obj.pageIndex = pageIndex;
            getJSON("Handler/DefectMsg.ashx", obj).then(function (res) {;
                if (res.success == "-1") {
                    window.location.href = "login.html";
                    return false;
                }
                _self.defectList = res.dataList;

                app.defectTotalRows = res.totalRows;
                app.defectTotalPage = res.totalPage;
            })
        },
        saveDefectInfo: function () {
            var _self = this;
            var obj = Object.assign({}, app.defectInfo);

            obj.type = 2;

            var r = confirm("确定添加该缺陷信息？")
            if (r == true) {
                getJSON("Handler/DefectMsg.ashx", obj).then(function (res) {

                    app.getDefectList(1);
                });
            }
            else {
                return false;
            }
        },
        getDefectContent: function (row) {
            var _self = this;

            var obj = {};
            obj.ID = row.ID;
            obj.defectNo = row.defectNo;
            obj.defectName = row.defectName;
            obj.type = 4;
            getJSON("Handler/DefectMsg.ashx", obj).then(function (res) {
                console.log(res)
                var dataList = res.dataList[0];
                _self.defectInfo.status=dataList.status;
                _self.defectInfo.defectNo = dataList.defectNo;
                _self.defectInfo.defectName = dataList.defectName;
                _self.defectInfo.defectGrade = dataList.defectGrade;
                _self.defectInfo.demandID = dataList.demandID;
                _self.defectInfo.defectType = dataList.defectType;
                _self.defectInfo.caseID = dataList.caseID;
                _self.defectInfo.defectDescription = dataList.defectDescription;
                _self.defectInfo.finishDatetime = dataList.finishDatetime;
                _self.defectInfo.accessory = dataList.accessory;
                _self.defectInfo.remark = dataList.remark;
                _self.defectInfo.functionModule = dataList.functionModule;
                _self.defectInfo.functionPoint = dataList.functionPoint;
                 _self.defectInfo.yzcd = dataList.yzcd;
                 _self.defectInfo.devOP = dataList.devOP;
                 _self.defectInfo.testOP = dataList.testOP;
                 _self.defectInfo.defectSource = dataList.defectSource;
                 _self.defectInfo.defectFX = dataList.defectFX;
                 _self.defectInfo.findDatetime = dataList.findDatetime;
            });
        },
        deleteDefectInfo: function (defectNo, defectName,ID) {
            var _self = this;
            var obj = {}
            obj.defectNo = defectNo;
            obj.defectName = defectName;
            obj.ID = ID;

            obj.type = 3;

            var r = confirm("确定删除？")
            if (r == true) {

                getJSON("Handler/DefectMsg.ashx", obj).then(function (res) {

                    app.getDefectList(1);
                });

            }
            else {
                return false;
            }
        },
        //这个时候利用formatter就可以实现索引累加啦
        formatter:function(row, column ,cellValue) {
            //放回索引值
            //console.log(row, column, cellValue)
            console.log(this.forumPageSize, this.forumPageIndex, row.rowIndex)
            return this.forumPageSize * (this.forumPageIndex - 1) + row.rowIndex;
        },
    }
}).$mount("#app");

app.getTestCaseList(tabsId);//初始化 需求列表
