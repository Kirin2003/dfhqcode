<template>
  <el-table
    class="essays"
    :data="CurrentEssays"
    row-style="height: 100px"
    @row-click="RouteToEssayInfo"
  >
    <el-table-column prop="title" label="题目" />
    <el-table-column prop="writers" label="作者" />
  </el-table>
  <el-footer>
    <div>
      <el-pagination
        :page-size="100"
        layout="total, prev, pager, next"
        :total="400"
        @current-change="handleCurrentChange"
      >
      </el-pagination>
    </div>
  </el-footer>
</template>
<script>
import router from "@/router/index";
import store from "@/store/";
export default {
  name: "EssayList",
  data() {
    return {
      AllEssays: [],
      CurrentEssays: [
        {
          Id: "134153413",
          title:
            "Image-to-Image Translation with Conditional Adversarial Networks",
          Authors: "Phillip Isola, Jun-Yan Zhu, Tinghui Zhou, Alexei A. Efros",
        },
        {
          title:
            "Unpaired Image-to-Image Translation Using Cycle-Consistent Adversarial Networks",
          writers: "Jun-Yan Zhu, Taesung Park Alexei A. Efros 2017",
        },
        {
          title:
            "Very Deep Convolutional Networks for Large-Scale Image Recognition",
          writers: "K. Simonyan, Andrew Zisserman",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
      ],
    };
  },
  methods: {
    RouteToEssayInfo(row) {
      this.essaySelected = row;
      store.commit("SelectEssay", this.essaySelected);
      //还要传递论文信息
      router.push({
        name: "Essay",
        params: { essayId: this.essaySelected.Id },
      });
    },
  },
};
</script>
<style scoped>
.essays {
  font-size: 20px;
}
.sort {
  font-size: 20px;
  font-weight: bold;
  margin-top: 0px;
}
.el-pagination {
  padding-left: 400px;
  padding-top: 20px;
  --el-pagination-font-size: 17px;
}
</style>
